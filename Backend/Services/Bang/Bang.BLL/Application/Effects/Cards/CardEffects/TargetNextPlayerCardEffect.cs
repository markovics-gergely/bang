using Bang.DAL.Domain;
using Bang.DAL.Domain.Constants.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class TargetNextPlayerCardEffect : TargetPlayerCardEffect
    {
        public TargetNextPlayerCardEffect(TargetReason reason) : base(reason) { }

        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            var nextTarget = await query.PlayerStore.GetNextPlayerAliveByPlayerAsync(query.PlayerCard.PlayerId, cancellationToken);
            var newQuery = new CardEffectQuery(query.PlayerCard, nextTarget, query.GameBoardStore, query.CardStore, query.PlayerStore, query.AccountStore);
            await base.Execute(newQuery, cancellationToken);
        }
    }
}
