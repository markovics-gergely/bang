using Bang.DAL.Domain;
using Bang.DAL.Domain.Constants.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class BangCardEffect : TargetPlayerCardEffect
    {
        public BangCardEffect() : base(DAL.Domain.Constants.Enums.TargetReason.Bang) { }

        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            TargetReason = DAL.Domain.Constants.Enums.TargetReason.Bang;

            var gameboard = await query.GameBoardStore.GetGameBoardSimplifiedAsync(query.PlayerCard.Player.GameBoardId, cancellationToken);
            if (query.PlayerCard.PlayerId == gameboard.TargetedPlayerId)
            {
                var actualPlayer = gameboard.Players.FirstOrDefault(p => p.Id == gameboard.ActualPlayerId);
                var lastPlayed = actualPlayer.PlayedCards.Last();
                if (lastPlayed == CardType.Duel)
                {
                    if (gameboard.LastTargetedPlayerId != null)
                    {
                        var lastTargeted = await query.PlayerStore.GetPlayerAsync((long)gameboard.LastTargetedPlayerId, cancellationToken);
                        query.TargetPlayer = lastTargeted;
                    }
                    else
                    {
                        query.TargetPlayer = actualPlayer;
                    }
                    await query.GameBoardStore.SetGameBoardLastTargetedPlayerAsync(query.PlayerCard.PlayerId, cancellationToken);
                    TargetReason = DAL.Domain.Constants.Enums.TargetReason.Duel;
                }
                else if (lastPlayed == CardType.Gatling || lastPlayed == CardType.Indians)
                {
                    var next = await query.PlayerStore.GetNextPlayerAliveByPlayerAsync(query.PlayerCard.PlayerId, cancellationToken);
                    if (next.Id == actualPlayer.Id)
                    {
                        TargetReason = null;
                        query.TargetPlayer = null;
                    }
                    else
                    {
                        if (lastPlayed == CardType.Gatling)
                        {
                            TargetReason = DAL.Domain.Constants.Enums.TargetReason.Gatling;
                        }
                        else if (lastPlayed == CardType.Indians)
                        {
                            TargetReason = DAL.Domain.Constants.Enums.TargetReason.Indians;
                        }
                        query.TargetPlayer = next;
                    }
                }
            }
            await base.Execute(query, cancellationToken);
        }
    }
}
