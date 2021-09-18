using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Joins.PlayerCards;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface ICardStore
    {
        Task<Card> GetCardByTypeAsync(CardType type, CancellationToken cancellationToken);
        Task<PlayerCard> GetPlayerCardAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Card>> GetCardsAsync(CancellationToken cancellationToken);

        Task PlayCardAsync(CardType type, long playerId, CancellationToken cancellationToken);
        Task<long> CreatePlayerCardAsync(PlayerCard playerCard, CancellationToken cancellationToken);
        Task<long> PlaceHandPlayerCardToTableAsync(HandPlayerCard playerCard, CancellationToken cancellationToken);
        Task<IEnumerable<long>> CreatePlayerCardsAsync(IEnumerable<PlayerCard> playerCards, CancellationToken cancellationToken);
        Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken);
        Task<IEnumerable<long>> CreateGameBoardCardsAsync(IEnumerable<GameBoardCard> gameBoardCards, CancellationToken cancellationToken);
        Task DeletePlayerCardAsync(long playerCardId, CancellationToken cancellationToken);
    }
}
