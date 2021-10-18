using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class ActiveCardEffect : CardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            await query.CardStore.PlacePlayerCardToDiscardedAsync(query.PlayerCard, cancellationToken);
        }
    }
}
