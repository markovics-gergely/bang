using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class PlayCardCommand : IRequest
    {
        public PlayCardDto PlayCardDto { get; set; }

        public PlayCardCommand(PlayCardDto playCardDto)
        {
            PlayCardDto = playCardDto;
        }
    }
}
