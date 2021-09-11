using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UserIdentity.BLL.Infrastructure.Stores
{
    public class AccountStore : IAccountStore
    {
        private readonly UserIdentityDbContext _dbContext;
        private readonly UserManager<Account> _userManager;

        public AccountStore(UserIdentityDbContext dbContext, UserManager<Account> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<bool> CreateAccountAsync(Account account, string password, CancellationToken cancellationToken)
        {
            if (await _dbContext.Users.AnyAsync(u => u.UserName == account.UserName, cancellationToken))
            {
                throw new InvalidParameterException("A felhasználónév már foglalt!");
            }

            var result = await _userManager.CreateAsync(account, password);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result.Succeeded;
        }

        public async Task LoginAccountAsync(Account account, string password, CancellationToken cancellationToken)
        {
        }

        public async Task DeleteAccountAsync(CancellationToken cancellationToken)
        {

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
