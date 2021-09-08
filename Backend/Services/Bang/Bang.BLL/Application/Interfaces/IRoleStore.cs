using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IRoleStore
    {
        Task<Role> GetRoleAsync(long id, CancellationToken cancellationToken);
        Task<Role> GetRoleByTypeAsync(RoleType type, CancellationToken cancellationToken);
        Task<IEnumerable<Role>> GetRolesAsync(CancellationToken cancellationToken);
    }
}
