using Bang.DAL.Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class TargetPlayerCardEffect : ActiveCardEffect
    {
        public TargetReason? TargetReason { get; set; }

        public TargetPlayerCardEffect(TargetReason reason)
        {
            TargetReason = reason;
        }

        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            if (query.TargetPlayer == null) throw new ArgumentNullException(nameof(query), $"{nameof(TargetReason)} TargetPlayer not set");
            await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(query.TargetPlayer.Id, cancellationToken);
            await query.GameBoardStore.SetGameBoardTargetReasonAsync(TargetReason, cancellationToken);
            await base.Execute(query, cancellationToken);
        }
    }
}
