using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins.PlayerCards;

using System.Collections.Generic;


namespace Bang.DAL.Domain
{
    public class Player
    {
        public long Id { get; set; }
        public string UserId { get; set; }

        public long GameBoardId { get; set; }
        public GameBoard GameBoard { get; set; }

        public CharacterType CharacterType { get; set; }
        public RoleType RoleType { get; set; }

        public int ActualHP { get; set; }
        public int MaxHP { get; set; }
        public int ShootingRange { get; set; } = 1;
        public int Placement { get; set; } = 0;

        public ICollection<HandPlayerCard> HandPlayerCards { get; set; } = new List<HandPlayerCard>();
        public ICollection<TablePlayerCard> TablePlayerCards { get; set; } = new List<TablePlayerCard>();
    }
}
