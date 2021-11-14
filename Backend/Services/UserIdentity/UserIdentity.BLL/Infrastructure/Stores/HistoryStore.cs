using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using UserIdentity.DAL.Domain.Bang;
using System;

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

        public async Task<IEnumerable<History>> GetHistoriesAsync(string ownId, CancellationToken cancellationToken)
        {
            return await _dbContext.Histories
                .Where(history => history.AccountId == ownId)
                .OrderBy(history => history.CreatedAt)
                .Take(10)
                .ToListAsync(cancellationToken);
        }

        public async Task CreateHistoryAsync(string ownId, RoleType playedRole, int placement, CancellationToken cancellationToken)
        {
            var history = new History
            {
                AccountId = ownId,
                PlayedRole = playedRole,
                Placement = placement,
                CreatedAt = DateTime.Now
            };

            await _dbContext.Histories.AddAsync(history, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
