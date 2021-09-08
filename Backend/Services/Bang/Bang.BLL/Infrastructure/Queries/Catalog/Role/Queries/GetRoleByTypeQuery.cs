using Bang.BLL.Infrastructure.Queries.Catalog.Role.ViewModels;
using Bang.DAL.Domain.Constants.Enums;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Role.Queries
{
    public class GetRoleByTypeQuery : IRequest<RoleViewModel>
    {
        public RoleType Type { get; set; }

        public GetRoleByTypeQuery(RoleType type)
        {
            Type = type;
        }
    }
}
