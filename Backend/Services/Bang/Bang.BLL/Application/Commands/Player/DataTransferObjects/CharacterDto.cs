using Bang.DAL.Domain.Constants.Enums;
using System.Collections.Generic;

namespace Bang.BLL.Application.Commands.Player.DataTransferObjects
{
    public class CharacterDto
    {
        public CharacterType CharacterType { get; set; }
        public long? TargetPlayerId { get; set; }
        public IEnumerable<long> CardIds { get; set; }
    }
}
