using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Commands.ViewModels
{
    public class PlayerCardViewModel
    {
        public long PlayerId { get; set; }
        public int CardId { get; set; }
        public CardColorType CardColorType { get; set; }
        public int FrenchNumber { get; set; }
    }
}
