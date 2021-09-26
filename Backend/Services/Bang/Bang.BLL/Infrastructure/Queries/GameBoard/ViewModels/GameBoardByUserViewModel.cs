using Bang.BLL.Application.Commands.ViewModels;
using Bang.DAL.Domain.Joins.GameBoardCards;
using System.Collections.Generic;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class GameBoardByUserViewModel
    {
        public long Id { get; set; }
        public long ActualPlayerId { get; set; }
        public long TargetedPlayerId { get; set; }
        public int MaxTurnTime { get; set; }
        public bool IsOver { get; set; }

        public PlayerViewModel OwnPlayer { get; set; }
        public ICollection<PlayerByUserViewModel> OtherPlayers { get; set; }
        public DiscardedGameBoardCard LastDiscardedGameBoardCard { get; set; }
    }
}
