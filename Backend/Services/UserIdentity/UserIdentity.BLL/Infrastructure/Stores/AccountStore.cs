using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace UserIdentity.BLL.Infrastructure.Stores
{
    public class AccountStore : IAccountStore
    {
        private readonly UserIdentityDbContext _dbContext;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly HttpContext _httpContext;

        public AccountStore(UserIdentityDbContext dbContext, UserManager<Account> userManager, SignInManager<Account> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<bool> CreateAccountAsync(Account account, string password, CancellationToken cancellationToken)
        {
            if (await _dbContext.Users.AnyAsync(u => u.UserName == account.UserName, cancellationToken))
            {
                throw new InvalidParameterException("A felhasználónév már foglalt!");
            }

            var result = await _userManager.CreateAsync(account, password);

            if(result.Errors.Count() != 0)
            {
                StringBuilder error = new StringBuilder();

                foreach(var e in result.Errors)
                {
                    error.Append($"{e.Description}\n");
                }

                throw new InvalidParameterException(error.ToString());
            }

            return result.Succeeded;
        }

        public async Task DeleteAccountAsync(Account account, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(account.Id);

            if (user == null)
            {
                throw new InvalidParameterException("User not found!");
            }

            await _userManager.DeleteAsync(user);
        }

        public async Task<string> GetAccountIdByName(string name, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user == null)
            {
                throw new InvalidParameterException("User not found!");
            }

            return user.Id;
        }

        public string GetActualAccountId()
        {
            return _httpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }
    }
}
