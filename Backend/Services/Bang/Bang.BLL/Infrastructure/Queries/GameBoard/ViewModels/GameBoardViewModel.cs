using Bang.BLL.Application.Commands.ViewModels;

using System.Collections.Generic;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class GameBoardViewModel
    {
        public long Id { get; set; }
        public long ActualPlayerId { get; set; }
        public int MaxTurnTime { get; set; }
        public bool IsOver { get; set; }

        public ICollection<PlayerViewModel> Players { get; set; }
    }
}
