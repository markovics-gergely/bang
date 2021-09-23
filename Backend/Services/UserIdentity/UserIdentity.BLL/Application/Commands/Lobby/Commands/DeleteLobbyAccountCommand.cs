using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class DeleteLobbyAccountCommand : IRequest
    {
        public long LobbyId { get; set; }

        public DeleteLobbyAccountCommand(long lobbyId)
        {
            LobbyId = lobbyId;
        }
    }
}
