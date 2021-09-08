using Bang.BLL.Infrastructure.Queries.Catalog.Role.ViewModels;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Role.Queries
{
    public class GetRoleQuery : IRequest<RoleViewModel>
    {
        public int Id { get; set; }

        public GetRoleQuery(int id)
        {
            Id = id;
        }
    }
}
