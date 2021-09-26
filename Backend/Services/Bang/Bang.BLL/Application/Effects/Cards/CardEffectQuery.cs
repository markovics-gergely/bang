using Bang.DAL;
using Bang.DAL.Domain;

namespace Bang.BLL.Application.Effects.Cards
{
    public class CardEffectQuery
    {
        public Player Player { get; set; }

        public CardEffectQuery(Player player)
        {
            Player = player;
        }
        public CardEffectQuery()
        {
        }
    }
}
