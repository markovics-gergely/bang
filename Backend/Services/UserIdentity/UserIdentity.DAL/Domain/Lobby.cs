using System.Collections.Generic;

namespace UserIdentity.DAL.Domain
{
    public class Lobby
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string OwnerId { get; set; }
        public Account Owner { get; set; }
        public ICollection<LobbyAccount> LobbyAccounts { get; set; } = new List<LobbyAccount>();
    }
}
