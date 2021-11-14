using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Interfaces
{
    public interface IFriendStore
    {
        Task<IEnumerable<Friend>> GetFriendsAsync(string ownId, CancellationToken cancellationToken);
        Task CreateFriendAsync(string ownId, string friendId, CancellationToken cancellationToken);
        Task DeleteFriendAsync(string ownId, string friendId, CancellationToken cancellationToken);
        Task UpdateIsInviteAsync(string senderId, string receiverId, bool isInvite, CancellationToken cancellationToken);
        Task UpdateIsInviteAsync(string senderId, bool isInvite, CancellationToken cancellationToken);
        Task UpdateIsInviteForAccountsAsync(List<LobbyAccount> accounts, bool isInvite, CancellationToken cancellationToken);
    }
}
