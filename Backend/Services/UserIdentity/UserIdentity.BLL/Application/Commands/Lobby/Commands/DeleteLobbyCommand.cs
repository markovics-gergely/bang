using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class DeleteLobbyCommand : IRequest
    {
        public long Id { get; set; }

        public DeleteLobbyCommand(long id)
        {
            Id = id;
        }
    }
}
