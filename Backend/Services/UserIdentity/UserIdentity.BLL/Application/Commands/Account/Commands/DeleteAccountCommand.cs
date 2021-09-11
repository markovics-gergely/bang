using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class DeleteAccountCommand : IRequest
    {
        public string Id { get; set; }

        public DeleteAccountCommand(string id)
        {
            Id = id;
        }
    }
}
