using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateFriendCommand : IRequest
    {
        public string Name { get; set; }

        public CreateFriendCommand(string name)
        {
            Name = name;
        }
    }
}
