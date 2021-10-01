using MediatR;
using UserIdentity.BLL.Application.Commands.DataTransferObjects;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateGameBoardCommand : IRequest
    {
        public int LobbyId;
        public GameBoardDto Dto;

        public CreateGameBoardCommand(int lobbyId, GameBoardDto dto)
        {
            LobbyId = lobbyId;
            Dto = dto;
        }
    }
}
