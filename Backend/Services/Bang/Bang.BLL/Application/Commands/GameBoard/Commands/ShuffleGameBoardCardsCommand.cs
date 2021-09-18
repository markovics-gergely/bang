using MediatR;

namespace Bang.BLL.Application.Commands.Commands
{
    public class ShuffleGameBoardCardsCommand : IRequest
    {
        public long gameBoardId { get; set; }

        public ShuffleGameBoardCardsCommand(long gameBoardId)
        {
            this.gameBoardId = gameBoardId;
        }
    }
}
