using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DecrementPlayerHealthCommand : IRequest
    {
        public long PlayerId { get; set; }

        public DecrementPlayerHealthCommand(long playerId)
        {
            PlayerId = playerId;
        }
    }
}
