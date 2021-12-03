using Bang.BLL.Application.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Characters.CharacterEffects
{
    public class SidKetchumCharacterEffect : CharacterEffect
    {
        public override async Task Execute(CharacterEffectQuery query, CancellationToken cancellationToken)
        {
            if (query.CharacterDto.CardIds.Count() != 2)
            {
                throw new NotEnoughCardException("Sid Ketchum needs 2 cards to discard!");
            }
            else if (query.CharacterDto.TargetPlayerId == null)
            {
                throw new NotEnoughPlayerException("Sid Ketchum needs own player id!");
            }
            foreach (var cardId in query.CharacterDto.CardIds)
            {
                var playerCard = await query.CardStore.GetPlayerCardAsync(cardId, cancellationToken);
                await query.CardStore.PlacePlayerCardToDiscardedAsync(playerCard, cancellationToken);
            }
            await query.PlayerStore.IncrementPlayerHealthAsync(cancellationToken);
        }
    }
}
