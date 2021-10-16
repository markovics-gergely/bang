using Bang.DAL.Domain.Constants.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class BangCardEffect : ActiveCardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            if (query.TargetPlayer == null) throw new ArgumentNullException(nameof(query), "Bang TargetPlayer not set");
            await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(query.TargetPlayer.Id, cancellationToken);
            await query.GameBoardStore.SetGameBoardTargetReasonAsync(TargetReason.Bang, cancellationToken);
            await base.Execute(query, cancellationToken);
        }
    }
}
