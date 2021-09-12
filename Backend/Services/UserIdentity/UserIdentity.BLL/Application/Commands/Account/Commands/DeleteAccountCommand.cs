using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class DeleteAccountCommand : IRequest
    {
        public string UserName { get; set; }

        public DeleteAccountCommand(string userName)
        {
            UserName = userName;
        }
    }
}
