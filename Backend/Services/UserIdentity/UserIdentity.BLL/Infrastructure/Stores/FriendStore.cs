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
    public class FriendStore : IFriendStore
    {
        private readonly UserIdentityDbContext _dbContext;

        public FriendStore(UserIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Friend>> GetFriendsAsync(string ownId, CancellationToken cancellationToken)
        {
            return await _dbContext.Friends.Where(user => 
                    user.SenderId == ownId || user.ReceiverId == ownId
                ).ToListAsync(cancellationToken);
        }

        public async Task AddFriendAsync(string ownId, string friendId, CancellationToken cancellationToken)
        {
            Friend friend = new Friend()
            {
                SenderId = ownId,
                ReceiverId = friendId
            };

            await _dbContext.Friends.AddAsync(friend, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteFriendAsync(string ownId, string friendId, CancellationToken cancellationToken)
        {
            var friends = _dbContext.Friends.Where(user => 
                    (user.SenderId == ownId && user.ReceiverId == friendId) ||
                    (user.SenderId == friendId && user.ReceiverId == ownId)
                );

            if(friends != null || friends.Count() != 0)
                _dbContext.Friends.RemoveRange(friends);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
