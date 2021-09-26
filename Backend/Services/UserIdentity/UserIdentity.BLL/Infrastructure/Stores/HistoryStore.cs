using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace UserIdentity.BLL.Infrastructure.Stores
{
    public class HistoryStore : IHistoryStore
    {
        private readonly UserIdentityDbContext _dbContext;
        private readonly IAccountStore _accountStore;

        public HistoryStore(UserIdentityDbContext dbContext, IAccountStore accountStore)
        {
            _dbContext = dbContext;
            _accountStore = accountStore;
        }

        public async Task<IEnumerable<Friend>> GetFriendsAsync(string ownId, CancellationToken cancellationToken)
        {
            return await _dbContext.Friends
                .Include(r => r.Sender)
                .Include(s => s.Receiver)
                .Where(user => user.SenderId == ownId || user.ReceiverId == ownId)
                .ToListAsync(cancellationToken);
        }
    }
}
