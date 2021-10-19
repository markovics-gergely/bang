using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetGameBoardByUserIdQuery : IRequest<GameBoardByUserViewModel>
    {
        public string UserId { get; set; }

        public GetGameBoardByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
