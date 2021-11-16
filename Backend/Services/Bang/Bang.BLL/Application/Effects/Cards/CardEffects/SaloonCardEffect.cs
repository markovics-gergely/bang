using Bang.DAL.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class SaloonCardEffect : ActiveCardEffect
    {
        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            var players = await query.PlayerStore.GetPlayersAliveByGameBoardAsync(cancellationToken);
            foreach (var player in players)
            {
                await query.PlayerStore.IncrementPlayerHealthAsync(player.Id, cancellationToken);
            }
            await base.Execute(query, cancellationToken);
        }
    }
}
