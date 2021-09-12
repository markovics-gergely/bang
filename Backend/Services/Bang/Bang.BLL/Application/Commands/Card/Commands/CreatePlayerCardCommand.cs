using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class CreatePlayerCardCommand : IRequest<long>
    {
        public PlayerCardDto Dto { get; set; }

        public CreatePlayerCardCommand(PlayerCardDto playCardDto)
        {
            Dto = playCardDto;
        }
    }
}
