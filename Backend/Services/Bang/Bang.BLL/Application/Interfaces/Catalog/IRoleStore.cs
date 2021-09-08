using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Interfaces.Catalog
{
    public interface IRoleStore
    {
        Task<Role> GetRoleAsync(long id, CancellationToken cancellationToken);
        Task<Role> GetRoleByTypeAsync(RoleType type, CancellationToken cancellationToken);
        Task<IEnumerable<Role>> GetRolesAsync(CancellationToken cancellationToken);
    }
}
