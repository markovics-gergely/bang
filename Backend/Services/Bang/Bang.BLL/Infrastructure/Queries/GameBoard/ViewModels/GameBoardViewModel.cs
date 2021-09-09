namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class GameBoardViewModel
    {
        public long Id { get; set; }
        public long ActualPlayerId { get; set; }
        public int MaxTurnTime { get; set; }
        public bool IsOver { get; set; } = false;
    }
}
