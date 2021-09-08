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

        public async Task<IEnumerable<Friend>> GetFriendsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Friends.Where(user => 
                    user.SenderId == "Bejelntkezett felhasznalo" || user.ReceiverId == "Bejelntkezett felhasznalo"
                ).ToListAsync(cancellationToken);
        }

        public async Task AddFriendsAsync(string friendId, CancellationToken cancellationToken)
        {
            Friend friend = new Friend()
            {
                SenderId = "Bejelentkezett felhasználó",
                ReceiverId = friendId
            };

            await _dbContext.Friends.AddAsync(friend, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteFriendsAsync(string friendId, CancellationToken cancellationToken)
        {
            var friends = _dbContext.Friends.Where(user => 
                    (user.SenderId == "Bejelentkezett felhasználó" && user.ReceiverId == friendId) ||
                    (user.SenderId == friendId && user.ReceiverId == "Bejelentkezett felhasználó")
                );

            if(friends != null || friends.Count() != 0)
                _dbContext.Friends.RemoveRange(friends);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
