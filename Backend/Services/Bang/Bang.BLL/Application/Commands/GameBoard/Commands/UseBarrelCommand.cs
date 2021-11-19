using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class UseBarrelCommand : IRequest
    {
        public long GameBoardId { get; set; }

        public UseBarrelCommand(long gameBoardId)
        {
            GameBoardId = gameBoardId;
        }
    }
}
