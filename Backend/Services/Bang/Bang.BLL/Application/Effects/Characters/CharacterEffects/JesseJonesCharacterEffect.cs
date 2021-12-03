using Bang.BLL.Application.Exceptions;
using Bang.DAL.Domain.Joins.PlayerCards;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Effects.Characters.CharacterEffects
{
    public class JesseJonesCharacterEffect : CharacterEffect
    {
        public override async Task Execute(CharacterEffectQuery query, CancellationToken cancellationToken)
        {
            if (query.CharacterDto.TargetPlayerId == null)
            {
                throw new NotEnoughPlayerException("Jesse Jones needs own player id!");
            }
            var player = await query.PlayerStore.GetPlayerAsync((long)query.CharacterDto.TargetPlayerId, cancellationToken);
            var ownPlayer = (await query.PlayerStore.GetOwnPlayerAsync(cancellationToken)).Id;
            if (player.HandPlayerCards.Count > 0)
            {
                var rnd = new Random();
                HandPlayerCard drawable = player.HandPlayerCards.OrderBy(h => rnd.Next()).Take(1).FirstOrDefault();
                await query.CardStore.PlacePlayerCardToHandAsync(drawable, ownPlayer, cancellationToken);
            }
            await query.GameBoardStore.DrawGameBoardCardsFromTopAsync(1, ownPlayer, cancellationToken);
            await query.GameBoardStore.SetGameBoardPhaseAsync(DAL.Domain.Constants.Enums.PhaseEnum.Playing, cancellationToken);
        }
    }
}
