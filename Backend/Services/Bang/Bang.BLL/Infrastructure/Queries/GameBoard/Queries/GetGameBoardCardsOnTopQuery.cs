using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetGameBoardCardsOnTopQuery : IRequest<IEnumerable<FrenchCardViewModel>>
    {
        public long Id { get; set; }
        public int Count { get; set; }

        public GetGameBoardCardsOnTopQuery(long id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}
