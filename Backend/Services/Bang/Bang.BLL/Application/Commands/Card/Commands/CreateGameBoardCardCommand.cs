using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class CreateGameBoardCardCommand : IRequest<long>
    {
        public GameBoardCardDto Dto { get; set; }

        public CreateGameBoardCardCommand(GameBoardCardDto gameBoardCardDto)
        {
            Dto = gameBoardCardDto;
        }
    }
}
