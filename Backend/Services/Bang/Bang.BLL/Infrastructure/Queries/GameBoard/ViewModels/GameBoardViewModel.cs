using Bang.DAL.Domain.Constants.Enums;
using System.Collections.Generic;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class GameBoardViewModel
    {
        public long Id { get; set; }
        public string LobbyOwnerId { get; set; }
        public long ActualPlayerId { get; set; }
        public long TargetedPlayerId { get; set; }
        public TargetReason? TargetReason { get; set; }

        public PhaseEnum TurnPhase { get; set; }
        public int MaxTurnTime { get; set; }
        public bool IsOver { get; set; }

        public ICollection<PlayerViewModel> Players { get; set; }
        public ICollection<FrenchCardViewModel> ScatteredGameBoardCards { get; set; }
    }
}
