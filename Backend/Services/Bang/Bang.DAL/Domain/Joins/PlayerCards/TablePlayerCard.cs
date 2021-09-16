using Bang.DAL.Domain.Catalog.Cards;

namespace Bang.DAL.Domain.Joins.PlayerCards
{
    public class TablePlayerCard : PlayerCard
    {
        public new PassiveCard Card { get; set; }
    }
}
