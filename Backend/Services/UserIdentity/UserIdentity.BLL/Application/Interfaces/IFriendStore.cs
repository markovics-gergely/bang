using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Interfaces
{
    public interface IFriendStore
    {
        Task<IEnumerable<Friend>> GetFriendsAsync(CancellationToken cancellationToken);
        Task AddFriendsAsync(string friendId, CancellationToken cancellationToken);
        Task DeleteFriendsAsync(string friendId, CancellationToken cancellationToken);
    }
}
