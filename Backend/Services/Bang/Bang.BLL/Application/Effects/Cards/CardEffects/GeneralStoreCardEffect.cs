using Bang.DAL.Domain;
using Bang.DAL.Domain.Constants.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Cards.CardEffects
{
    public class GeneralStoreCardEffect : TargetPlayerCardEffect
    {
        public GeneralStoreCardEffect() : base(DAL.Domain.Constants.Enums.TargetReason.GeneralStore) { }

        public override async Task Execute(CardEffectQuery query, CancellationToken cancellationToken)
        {
            await query.GameBoardStore.DrawGameBoardCardsToScatteredByPlayersAliveAsync(cancellationToken);
            var actual = query.PlayerCard.Player;
            query.TargetPlayer = actual;
            await base.Execute(query, cancellationToken);
        }
    }
}
