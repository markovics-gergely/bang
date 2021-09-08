using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces.Catalog;
using Bang.DAL;
using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Constants.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Stores
{
    public class RoleStore : IRoleStore
    {
        private readonly BangDbContext _dbContext;

        public RoleStore(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetRoleAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Role not found!");
        }

        public async Task<Role> GetRoleByTypeAsync(RoleType type, CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.Where(c => c.RoleType == type).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Role not found!");
        }

        public async Task<IEnumerable<Role>> GetRolesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.ToListAsync(cancellationToken);
        }
    }
}
