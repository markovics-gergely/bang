using Bang.BLL.Application.Commands.DataTransferObjects;

using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class CreateGameBoardCommand : IRequest<long>
    {
        public GameBoardDto Dto { get; set; }

        public CreateGameBoardCommand(GameBoardDto dto)
        {
            Dto = dto;
        }
    }
}
