using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class DeleteFriendCommand : IRequest
    {
        public string Id { get; set; }

        public DeleteFriendCommand(string id)
        {
            Id = id;
        }
    }
}
