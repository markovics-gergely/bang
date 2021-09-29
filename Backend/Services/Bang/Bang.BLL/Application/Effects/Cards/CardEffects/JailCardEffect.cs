using Bang.DAL.Domain.Constants.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class JailCardEffect : PassiveCardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            if (query.TargetPlayer == null) throw new ArgumentNullException("Jail TargetPlayer not set");
            if (query.TargetPlayer.RoleType == RoleType.Sheriff) throw new ArgumentException("Sheriff cannot be in jail");
            await query.CardStore.PlaceHandPlayerCardToAnotherTableAsync(query.PlayerCard, query.TargetPlayer, cancellationToken);
        }
    }
}
