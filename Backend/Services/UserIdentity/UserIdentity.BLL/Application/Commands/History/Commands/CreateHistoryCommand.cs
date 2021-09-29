using UserIdentity.DAL.Domain.Bang;

using MediatR;

namespace UserIdentity.BLL.Application.Commands.Commands
{
    public class CreateHistoryCommand : IRequest
    {
        public RoleType PlayedRole { get; set; }
        public int Placement { get; set; }

        public CreateHistoryCommand(RoleType playedRole, int placement)
        {
            PlayedRole = playedRole;
            Placement = placement;
        }
    }
}
