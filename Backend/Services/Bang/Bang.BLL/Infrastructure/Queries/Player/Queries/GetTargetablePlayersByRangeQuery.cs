using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetTargetablePlayersByRangeQuery : IRequest<IEnumerable<PlayerViewModel>>
    {
        public long Id { get; set; }
        public int Range { get; set; }

        public GetTargetablePlayersByRangeQuery(long id, int range)
        {
            Id = id;
            Range = range;
        }
    }
}
