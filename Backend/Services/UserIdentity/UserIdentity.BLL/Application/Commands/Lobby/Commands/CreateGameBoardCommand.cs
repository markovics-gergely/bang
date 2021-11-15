using MediatR;
using UserIdentity.BLL.Application.Commands.DataTransferObjects;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateGameBoardCommand : IRequest
    {
        public long LobbyId { get; set; }
        public long GameBoardId { get; set; }

        public CreateGameBoardCommand(long lobbyId, long gameBoardId)
        {
            LobbyId = lobbyId;
            GameBoardId = gameBoardId;
        }
    }
}
