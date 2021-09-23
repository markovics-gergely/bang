using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.PlayerCards;

namespace Bang.BLL.Application.Effects.Cards
{
    public class CardEffectQuery
    {
        public HandPlayerCard PlayerCard { get; set; }
        public Player TargetPlayer { get; set; }
        public ICardStore CardStore { get; set; }

        public CardEffectQuery(HandPlayerCard playerCard, ICardStore cardStore)
        {
            PlayerCard = playerCard;
            CardStore = cardStore;
        }

        public CardEffectQuery(HandPlayerCard playerCard, Player player, ICardStore cardStore)
        {
            PlayerCard = playerCard;
            TargetPlayer = player;
            CardStore = cardStore;
        }
    }
}
