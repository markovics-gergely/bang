using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DrawCardFromPlayerCommand : IRequest
    {
        public long PlayerId { get; set; }

        public DrawCardFromPlayerCommand(long playerId)
        {
            PlayerId = playerId;
        }
    }
}
