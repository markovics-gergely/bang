using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class UpdateLobbyInviteFalseCommand : IRequest
    {
        public string AccountName { get; set; }

        public UpdateLobbyInviteFalseCommand(string accountName)
        {
            AccountName = accountName;
        }
    }
}
