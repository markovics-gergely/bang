using Bang.BLL.Application.Commands.ViewModels;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;

namespace Bang.BLL.Infrastructure.Queries.ViewModels
{
    public class PlayerViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public long GameBoardId { get; set; }
        public CharacterType CharacterType { get; set; }
        public RoleType RoleType { get; set; }
        public int ActualHP { get; set; }
        public int MaxHP { get; init; }

        public ICollection<FrenchCardViewModel> HandPlayerCards { get; set; }
        public ICollection<FrenchCardViewModel> TablePlayerCards { get; set; }
    }
}
