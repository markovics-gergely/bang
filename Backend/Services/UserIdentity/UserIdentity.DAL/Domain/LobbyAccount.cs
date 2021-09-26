namespace UserIdentity.DAL.Domain
{
    public class LobbyAccount
    {
        public long Id { get; set; }
        public long LobbyId { get; set; }
        public Lobby Lobby { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public bool IsConnected { get; set; }
    }
}
