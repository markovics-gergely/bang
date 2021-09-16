using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetTargetablePlayersQuery : IRequest<IEnumerable<PlayerViewModel>>
    {
        public long Id { get; set; }

        public GetTargetablePlayersQuery(long id)
        {
            Id = id;
        }
    }
}
