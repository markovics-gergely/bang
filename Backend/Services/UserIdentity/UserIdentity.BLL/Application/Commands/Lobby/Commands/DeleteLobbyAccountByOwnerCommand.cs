using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class DeleteLobbyAccountByOwnerCommand : IRequest
    {
        public long LobbyId { get; set; }
        public string AccountName { get; set; }

        public DeleteLobbyAccountByOwnerCommand(long lobbyId, string accountName)
        {
            LobbyId = lobbyId;
            AccountName = accountName;
        }
    }
}
