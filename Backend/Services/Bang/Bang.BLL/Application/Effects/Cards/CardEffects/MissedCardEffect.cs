using Bang.DAL.Domain.Constants.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class MissedCardEffect : ActiveCardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            var gameboard = await query.GameBoardStore.GetGameBoardAsync(query.PlayerCard.Player.GameBoardId, cancellationToken);
            if (query.PlayerCard.PlayerId == gameboard.ActualPlayerId  && gameboard.ActualPlayer.CharacterType == CharacterType.CalamityJanet)
            {
                if (query.TargetPlayer == null) throw new ArgumentNullException(nameof(query), "Missed (Bang) TargetPlayer not set");
                await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(query.TargetPlayer.Id, cancellationToken);
                await query.GameBoardStore.SetGameBoardTargetReasonAsync(TargetReason.Bang, cancellationToken);
            }
            else if (query.PlayerCard.PlayerId == gameboard.TargetedPlayerId)
            {
                var actualPlayer = gameboard.ActualPlayer;
                var lastPlayed = actualPlayer.PlayedCards.Last();
                if (lastPlayed == CardType.Duel && gameboard.ActualPlayer.CharacterType == CharacterType.CalamityJanet)
                {
                    await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(actualPlayer.Id, cancellationToken);
                    await base.Execute(query, cancellationToken);
                }
                else if (lastPlayed == CardType.Gatling && lastPlayed == CardType.Indians && gameboard.ActualPlayer.CharacterType == CharacterType.CalamityJanet)
                {
                    var next = await query.PlayerStore.GetNextPlayerAliveByPlayerAsync(query.PlayerCard.PlayerId, cancellationToken);
                    if (next.Id == actualPlayer.Id)
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(null, cancellationToken);
                        await query.GameBoardStore.SetGameBoardTargetReasonAsync(null, cancellationToken);
                    }
                    else
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(next.Id, cancellationToken);
                        if (lastPlayed == CardType.Gatling)
                        {
                            await query.GameBoardStore.SetGameBoardTargetReasonAsync(TargetReason.Gatling, cancellationToken);
                        }
                        else if (lastPlayed == CardType.Indians)
                        {
                            await query.GameBoardStore.SetGameBoardTargetReasonAsync(TargetReason.Indians, cancellationToken);
                        }
                    }
                }
                else if (lastPlayed == CardType.Bang)
                {
                    await query.GameBoardStore.SetGameBoardTargetedPlayerAsync(null, cancellationToken);
                    await query.GameBoardStore.SetGameBoardTargetReasonAsync(null, cancellationToken);
                }
            }
            await base.Execute(query, cancellationToken);
        }
    }
}
