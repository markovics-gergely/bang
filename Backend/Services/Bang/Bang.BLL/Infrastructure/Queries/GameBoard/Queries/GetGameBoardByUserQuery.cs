using Bang.BLL.Infrastructure.Queries.ViewModels;

using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetGameBoardByUserQuery : IRequest<GameBoardByUserViewModel>
    {
    }
}
