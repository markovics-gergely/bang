using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Commands.DataTransferObjects
{
    public class PlayCardDto
    {
        public long PlayerCardId { get; set; }
        public long? TargetPlayerId { get; set; }
        public long? TargetPlayerCardId { get; set; }
    }
}
