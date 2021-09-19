using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class DeleteFriendCommand : IRequest
    {
        public string Name { get; set; }

        public DeleteFriendCommand(string name)
        {
            Name = name;
        }
    }
}
