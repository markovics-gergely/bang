using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class PlayCardCommand : IRequest
    {
        public long HandPlayerCardId { get; set; }

        public PlayCardCommand(long handPlayerCardId)
        {
            HandPlayerCardId = handPlayerCardId;
        }
    }
}
