namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class PlayerViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long GameBoardId { get; set; }
        public string CharacterType { get; set; }
        public string RoleType { get; set; }
        public int ActualHP { get; set; }
        public int MaxHP { get; init; }
    }
}
