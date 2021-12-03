using Bang.BLL.Application.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Characters.CharacterEffects
{
    public class KitCarlsonCharacterEffect : CharacterEffect
    {
        public override async Task Execute(CharacterEffectQuery query, CancellationToken cancellationToken)
        {
            if (query.CharacterDto.CardIds.Count() != 2)
            {
                throw new NotEnoughCardException("Kit Carlson needs 2 cards to draw!");
            }
            else if (query.CharacterDto.TargetPlayerId == null)
            {
                throw new NotEnoughPlayerException("Kit Carlson needs own player id!");
            }
            foreach (var cardId in query.CharacterDto.CardIds)
            {
                await query.GameBoardStore.DrawGameBoardCardAsync(cardId, (long)query.CharacterDto.TargetPlayerId, cancellationToken);
            }
            await query.GameBoardStore.SetGameBoardPhaseAsync(DAL.Domain.Constants.Enums.PhaseEnum.Playing, cancellationToken);
        }
    }
}
