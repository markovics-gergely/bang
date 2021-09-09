using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetGameBoardQuery : IRequest<GameBoardViewModel>
    {
        public long Id { get; set; }

        public GetGameBoardQuery(long id)
        {
            Id = id;
        }
    }
}
