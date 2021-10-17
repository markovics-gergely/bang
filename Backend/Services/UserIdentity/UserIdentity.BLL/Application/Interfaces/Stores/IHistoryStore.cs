using UserIdentity.DAL.Domain;
using UserIdentity.DAL.Domain.Bang;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Interfaces
{
    public interface IHistoryStore
    {
        Task<IEnumerable<History>> GetHistoriesAsync(string ownId, CancellationToken cancellationToken);
        Task CreateHistoryAsync(string ownId, RoleType playedRole, int placement, CancellationToken cancellationToken);
    }
}
