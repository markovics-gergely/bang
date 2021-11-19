using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Queries
{
    public class GetPermissionsByGameBoardQuery : IRequest<PermissionViewModel>
    {
        public GameBoard GameBoard { get; set; }

        public GetPermissionsByGameBoardQuery(GameBoard gameBoard)
        {
            GameBoard = gameBoard;
        }
    }
}
