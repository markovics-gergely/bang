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
            var gameboard = await query.GameBoardStore.GetGameBoardSimplifiedAsync(query.PlayerCard.Player.GameBoardId, cancellationToken);
            var actualPlayer = gameboard.Players.FirstOrDefault(p => p.Id == gameboard.ActualPlayerId);

            if (query.PlayerCard.PlayerId == gameboard.TargetedPlayerId)
            {
                var lastPlayed = actualPlayer.PlayedCards.Last();
                if (lastPlayed == CardType.Duel && actualPlayer.CharacterType == CharacterType.CalamityJanet)
                {
                    await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(
                        actualPlayer.Id, TargetReason.Duel, cancellationToken);
                    await base.Execute(query, cancellationToken);
                }
                else if ((lastPlayed == CardType.Gatling || lastPlayed == CardType.Indians) && actualPlayer.CharacterType == CharacterType.CalamityJanet)
                {
                    var next = await query.PlayerStore.GetNextPlayerAliveByPlayerAsync(query.PlayerCard.PlayerId, cancellationToken);
                    if (next.Id == actualPlayer.Id)
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(null, null, cancellationToken);
                    }
                    else if (lastPlayed == CardType.Gatling)
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(next.Id, TargetReason.Gatling, cancellationToken);
                    }
                    else if (lastPlayed == CardType.Indians)
                    {
                        await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(next.Id, TargetReason.Indians, cancellationToken);
                    }
                }
                else if (lastPlayed == CardType.Bang)
                {
                    await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(null, null, cancellationToken);
                }
            }
            else if (query.PlayerCard.PlayerId == gameboard.ActualPlayerId && actualPlayer.CharacterType == CharacterType.CalamityJanet)
            {
                if (query.TargetPlayer != null)
                {
                    await query.GameBoardStore.SetGameBoardTargetedPlayerAndReasonAsync(
                        query.TargetPlayer.Id, TargetReason.Bang, cancellationToken);
                }
            }
            await base.Execute(query, cancellationToken);
        }
    }
}
