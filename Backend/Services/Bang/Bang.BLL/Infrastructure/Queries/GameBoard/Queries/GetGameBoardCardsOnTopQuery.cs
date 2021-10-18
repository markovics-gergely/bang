using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetGameBoardCardsOnTopQuery : IRequest<IEnumerable<FrenchCardViewModel>>
    {
        public int Count { get; set; }

        public GetGameBoardCardsOnTopQuery(int count)
        {
            Count = count;
        }
    }
}
