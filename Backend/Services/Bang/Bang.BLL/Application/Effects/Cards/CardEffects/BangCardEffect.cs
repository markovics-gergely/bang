using Bang.DAL.Domain.Constants.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class BangCardEffect : ActiveCardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            var gameboard = await query.GameBoardStore.GetGameBoardAsync(query.PlayerCard.Player.GameBoardId, cancellationToken);
            if (query.PlayerCard.PlayerId == gameboard.ActualPlayerId)
            {
                if (query.TargetPlayer == null) throw new ArgumentNullException(nameof(query), "Bang TargetPlayer not set");
                await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(query.TargetPlayer.Id, cancellationToken);
                await query.GameBoardStore.SetGameBoardTargetReasonAsync(TargetReason.Bang, cancellationToken);
            }
            else if (query.PlayerCard.PlayerId == gameboard.TargetedPlayerId)
            {
                var actualPlayer = gameboard.ActualPlayer;
                var lastPlayed = actualPlayer.PlayedCards.Last();
                if (lastPlayed == CardType.Duel)
                {
                    await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(actualPlayer.Id, cancellationToken);
                    await base.Execute(query, cancellationToken);
                }
                else if (lastPlayed == CardType.Gatling)
                {
                    var next = await query.PlayerStore.GetNextPlayerAliveByPlayerAsync(query.PlayerCard.PlayerId, cancellationToken);
                    if (next.Id == actualPlayer.Id)
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(null, cancellationToken);
                    }
                    else
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(next.Id, cancellationToken);
                    }
                }
            }
            await base.Execute(query, cancellationToken);
        }
    }
}
