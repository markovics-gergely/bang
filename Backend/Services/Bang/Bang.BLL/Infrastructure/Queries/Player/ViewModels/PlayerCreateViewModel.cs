using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class PlayerCreateViewModel
    {
        public string UserId { get; set; }
        public long GameBoardId { get; set; }
        public CharacterType CharacterType { get; set; }
        public RoleType RoleType { get; set; }
        public int ActualHP { get; set; }
        public int MaxHP { get; init; }
    }
}
