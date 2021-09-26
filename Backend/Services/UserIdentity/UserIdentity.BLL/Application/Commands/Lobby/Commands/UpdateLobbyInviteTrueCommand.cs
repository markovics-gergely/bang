using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class UpdateLobbyInviteTrueCommand : IRequest
    {
        public string AccountName { get; set; }

        public UpdateLobbyInviteTrueCommand(string accountName)
        {
            AccountName = accountName;
        }
    }
}
