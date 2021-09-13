using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins;
using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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

        public ICollection<PlayerCard> PlayerCards { get; set; } = new List<PlayerCard>();
    }
}
