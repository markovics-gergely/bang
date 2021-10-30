using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetGameBoardByUserSimplifiedQuery : IRequest<GameBoardByUserViewModel>
    {
        public string UserId { get; set; }

        public GetGameBoardByUserSimplifiedQuery(string userId)
        {
            UserId = userId;
        }
    }
}
