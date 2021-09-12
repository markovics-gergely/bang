using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Commands.DataTransferObjects
{
    public class PlayerCardDto
    {
        public long PlayerId { get; set; }
        public CardType CardType { get; set; }
    }
}
