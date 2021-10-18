using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class DiscardCardCommand : IRequest
    {
        public long PlayerCardId { get; set; }

        public DiscardCardCommand(long playerCardId)
        {
            PlayerCardId = playerCardId;
        }
    }
}
