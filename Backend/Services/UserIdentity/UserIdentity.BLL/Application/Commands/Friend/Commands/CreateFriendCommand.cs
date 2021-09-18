using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateFriendCommand : IRequest
    {
        public string Id { get; set; }

        public CreateFriendCommand(string id)
        {
            Id = id;
        }
    }
}
