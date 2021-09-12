using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserIdentity.BLL.Infrastructure.Stores
{
    public class AccountStore : IAccountStore
    {
        private readonly UserIdentityDbContext _dbContext;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        public AccountStore(UserIdentityDbContext dbContext, UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
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

        public async Task LoginAccountAsync(Account account, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(account.UserName);

            if(user == null)
            {
                throw new InvalidParameterException("Hibás felhasználónév!");
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var tokenLifetime = 120;

                var props = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifetime),
                    AllowRefresh = true,
                };

                await _signInManager.SignInAsync(user, props);
            }
            else
            {
                throw new InvalidParameterException("Hibás jelszó!");
            }
        }

        public async Task DeleteAccountAsync(Account account, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(account.UserName);

            if (user == null)
            {
                throw new InvalidParameterException("Nincs ilyen felhasználó!");
            }

            await _userManager.DeleteAsync(user);
        }
    }
}
