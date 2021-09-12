using Bang.DAL.Domain.Catalog.Cards;

namespace Bang.DAL.Domain.Joins
{
    public class PlayerCard
    {
        public int Id { get; set; }

        public long PlayerId { get; set; }
        public Player Player { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
