using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetLastDiscardedGameBoardCardQuery : IRequest<FrenchCardViewModel>
    {
        public long Id { get; set; }

        public GetLastDiscardedGameBoardCardQuery(long id)
        {
            Id = id;
        }
    }
}
