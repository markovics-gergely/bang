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
    public class FriendStore : IFriendStore
    {
        private readonly UserIdentityDbContext _dbContext;

        public FriendStore(UserIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Friend>> GetFriendsAsync(string ownId, CancellationToken cancellationToken)
        {
            return await _dbContext.Friends
                .Include(r => r.Sender)
                .Include(s => s.Receiver)
                .Where(user => user.SenderId == ownId || user.ReceiverId == ownId)
                .ToListAsync(cancellationToken);
        }

        public async Task CreateFriendAsync(string ownId, string friendId, CancellationToken cancellationToken)
        {
            if (ownId == friendId)
            {
                throw new InvalidParameterException("You can't add yourself!");
            }

            if (await _dbContext.Friends.AnyAsync(u => u.SenderId == ownId && u.ReceiverId == friendId))
            {
                throw new InvalidParameterException("You are already friends!");
            }

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
            if (ownId == friendId)
            {
                throw new EntityNotFoundException("You can't delete yourself from friends!");
            }

            var friends = await _dbContext.Friends
                .Where(user => 
                    (user.SenderId == ownId && user.ReceiverId == friendId) ||
                    (user.SenderId == friendId && user.ReceiverId == ownId))
                .ToListAsync(cancellationToken);

            if(friends.Count() == 0)
            {
                throw new EntityNotFoundException("Friend is not found!");
            }

            _dbContext.Friends.RemoveRange(friends);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateIsInviteAsync(string senderId, string receiverId, bool isInvite, CancellationToken cancellationToken)
        {
            var friend = await _dbContext.Friends.Where(f => f.SenderId == senderId && f.ReceiverId == receiverId).FirstOrDefaultAsync(cancellationToken);

            if(friend == null)
            {
                throw new EntityNotFoundException("Friend not found!");
            }

            friend.IsInvitedToGame = isInvite;

            var entry = _dbContext.Attach(friend);
            await _dbContext.SaveChangesAsync(cancellationToken);           
        }

        public async Task UpdateIsInviteAsync(string senderId, bool isInvite, CancellationToken cancellationToken)
        {
            var ownInvites = await _dbContext.Friends.Where(f => f.SenderId == senderId).ToListAsync(cancellationToken);

            foreach(var ownInvite in ownInvites)
                ownInvite.IsInvitedToGame = isInvite;

            var entry = _dbContext.Attach(ownInvites);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateIsInviteForAccountsAsync(List<LobbyAccount> accounts, bool isInvite, CancellationToken cancellationToken)
        {
            var invites = await _dbContext.Friends
                .Where(f => accounts.Select(s => s.AccountId).Contains(f.SenderId) || accounts.Select(s => s.AccountId).Contains(f.ReceiverId))
                .ToListAsync(cancellationToken);

            foreach (var invite in invites)
                invite.IsInvitedToGame = isInvite;

            var entry = _dbContext.Attach(invites);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
