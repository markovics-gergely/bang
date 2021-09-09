using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetPlayerQuery : IRequest<PlayerViewModel>
    {
        public long Id { get; set; }

        public GetPlayerQuery(long id)
        {
            Id = id;
        }
    }
}
