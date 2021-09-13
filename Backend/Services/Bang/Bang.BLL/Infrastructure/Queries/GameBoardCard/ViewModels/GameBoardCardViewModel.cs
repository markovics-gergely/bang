using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Commands.DataTransferObjects
{
    public class GameBoardCardViewModel
    {
        public long GameBoardId { get; set; }
        public long CardId { get; set; }
        public string StatusType { get; set; }
        public CardColorType CardColorType { get; set; }
        public int FrenchNumber { get; set; }
    }
}
