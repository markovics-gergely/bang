using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
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
