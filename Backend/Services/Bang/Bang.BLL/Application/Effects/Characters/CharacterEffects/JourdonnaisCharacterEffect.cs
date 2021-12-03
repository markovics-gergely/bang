using Bang.BLL.Application.Exceptions;
using Bang.DAL.Domain.Constants.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Characters.CharacterEffects
{
    public class JourdonnaisCharacterEffect : CharacterEffect
    {
        public override async Task Execute(CharacterEffectQuery query, CancellationToken cancellationToken)
        {
            var userId = query.AccountStore.GetActualAccountId();
            var gameboard = await query.GameBoardStore.GetGameBoardByUserSimplifiedAsync(userId, cancellationToken);
            var discarded = await query.GameBoardStore.DiscardFromDrawableGameBoardCardAsync(cancellationToken);
            if (discarded.CardColorType == CardColorType.Hearts)
            {
                if (gameboard.TargetReason == TargetReason.Bang)
                {
                    await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(null, null, cancellationToken);
                }
                else if (gameboard.TargetReason == TargetReason.Gatling && gameboard.TargetedPlayerId != null)
                {
                    var next = await query.PlayerStore.GetNextPlayerAliveByPlayerAsync((long)gameboard.TargetedPlayerId, cancellationToken);
                    if (next.Id == gameboard.ActualPlayerId)
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(null, null, cancellationToken);
                    }
                    else
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(next.Id, TargetReason.Gatling, cancellationToken);
                    }
                }
            }
        }
    }
}
