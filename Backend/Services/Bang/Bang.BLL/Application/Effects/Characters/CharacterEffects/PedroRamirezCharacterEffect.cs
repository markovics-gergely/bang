using Bang.BLL.Application.Exceptions;
using Bang.DAL.Domain.Joins.PlayerCards;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Characters.CharacterEffects
{
    public class PedroRamirezCharacterEffect : CharacterEffect
    {
        public override async Task Execute(CharacterEffectQuery query, CancellationToken cancellationToken)
        {
            var ownPlayer = (await query.PlayerStore.GetOwnPlayerAsync(cancellationToken)).Id;

            var last = await query.GameBoardStore.GetLastDiscardedGameBoardCardAsync(cancellationToken);
            await query.GameBoardStore.DrawGameBoardCardAsync(last.Id, ownPlayer, cancellationToken);

            await query.GameBoardStore.DrawGameBoardCardsFromTopAsync(1, ownPlayer, cancellationToken);
            await query.GameBoardStore.SetGameBoardPhaseAsync(DAL.Domain.Constants.Enums.PhaseEnum.Playing, cancellationToken);
        }
    }
}
