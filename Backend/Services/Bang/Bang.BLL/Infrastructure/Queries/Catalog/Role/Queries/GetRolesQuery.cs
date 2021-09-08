using System.Collections.Generic;
using MediatR;
using Bang.BLL.Infrastructure.Queries.Catalog.Role.ViewModels;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Role.Queries
{
    public class GetRolesQuery : IRequest<IEnumerable<RoleViewModel>>
    {
    }
}
