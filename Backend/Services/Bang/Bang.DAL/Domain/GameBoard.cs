using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins.GameBoardCards;

using System.Collections.Generic;

namespace Bang.DAL.Domain
{
    public class GameBoard
    {
        public long Id { get; set; }
        public string LobbyOwnerId { get; set; }

        public long? ActualPlayerId { get; set; }
        public Player ActualPlayer { get; set; }

        public long? TargetedPlayerId { get; set; }
        public Player TargetedPlayer { get; set; }
        public TargetReason? TargetReason { get; set; }

        public PhaseEnum TurnPhase { get; set; }
        public int MaxTurnTime { get; set; }
        public bool IsOver { get; set; } = false;

        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<DrawableGameBoardCard> DrawableGameBoardCards { get; set; } = new List<DrawableGameBoardCard>();
        public ICollection<DiscardedGameBoardCard> DiscardedGameBoardCards { get; set; } = new List<DiscardedGameBoardCard>();
        public ICollection<ScatteredGameBoardCard> ScatteredGameBoardCards { get; set; } = new List<ScatteredGameBoardCard>();
    }
}
