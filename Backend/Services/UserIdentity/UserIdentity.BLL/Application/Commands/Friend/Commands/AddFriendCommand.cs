using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class AddFriendCommand : IRequest
    {
        public string Id { get; set; }

        public AddFriendCommand(string id)
        {
            Id = id;
        }
    }
}
