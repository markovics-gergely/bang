using Bang.DAL.Domain.Constants.Enums;
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
            if (query.TargetPlayer != null)
            {
                await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(
                    query.TargetPlayer.Id, TargetReason, cancellationToken);
            }
            await base.Execute(query, cancellationToken);
        }
    }
}
