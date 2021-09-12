using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Commands.DataTransferObjects
{
    public class GameBoardCardDto
    {
        public long GameBoardId { get; set; }
        public CardType CardType { get; set; }
    }
}
