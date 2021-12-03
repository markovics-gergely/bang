using Bang.DAL.Domain.Constants.Enums;

namespace Bang.DAL.Domain.Catalog.Cards
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardEffectType { get; set; }
        public CardType CardType { get; set; }
    }
}
