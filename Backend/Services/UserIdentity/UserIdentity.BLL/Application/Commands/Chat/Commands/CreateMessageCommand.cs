using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateMessageCommand : IRequest
    {
        public string Message { get; set; }

        public CreateMessageCommand(string message)
        {
            Message = message;
        }
    }
}
