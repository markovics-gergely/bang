using Bang.DAL.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class BeerCardEffect : ActiveCardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            await query.PlayerStore.IncrementPlayerHealthAsync(cancellationToken);
            await base.Execute(query, cancellationToken);
        }
    }
}
