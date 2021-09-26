using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetGameBoardByUserQuery : IRequest<GameBoardByUserViewModel>
    {
        public string UserId { get; set; }

        public GetGameBoardByUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}
