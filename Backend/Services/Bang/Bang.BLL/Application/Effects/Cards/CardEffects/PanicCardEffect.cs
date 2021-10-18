using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins.PlayerCards;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class PanicCardEffect : ActiveCardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            if (query.TargetPlayer == null && query.TargetCard == null) throw new ArgumentNullException(nameof(query), "Cat Balou Target not set");
            var userId = query.AccountStore.GetActualAccountId();
            var ownPlayer = await query.PlayerStore.GetPlayerByUserIdAsync(userId, cancellationToken);
            if (query.TargetPlayer != null)
            {
                var player = query.TargetPlayer;
                if (player.HandPlayerCards.Count > 0)
                {
                    var rnd = new Random();
                    HandPlayerCard throwable = player.HandPlayerCards.OrderBy(h => rnd.Next()).Take(1).FirstOrDefault();
                    await query.CardStore.PlacePlayerCardToHandAsync(throwable, ownPlayer.Id, cancellationToken);
                }
            } 
            else
            {
                await query.CardStore.PlacePlayerCardToHandAsync(query.TargetCard, ownPlayer.Id, cancellationToken);
            }
            await base.Execute(query, cancellationToken);
        }
    }
}
