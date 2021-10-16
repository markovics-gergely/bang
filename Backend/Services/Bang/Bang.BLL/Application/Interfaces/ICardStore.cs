using Bang.DAL.Domain;
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
        Task<IEnumerable<PlayerCard>> GetPlayerCardsAsync(long playerId, CancellationToken cancellationToken);
        Task<IEnumerable<Card>> GetCardsAsync(CancellationToken cancellationToken);

        Task<long> CreatePlayerCardAsync(PlayerCard playerCard, CancellationToken cancellationToken);
        Task<long> PlaceHandPlayerCardToTableAsync(HandPlayerCard playerCard, CancellationToken cancellationToken);
        Task<long> PlaceHandPlayerCardToAnotherTableAsync(HandPlayerCard playerCard, Player targetPlayer, CancellationToken cancellationToken);
        Task<long> PlacePlayerCardToDiscardedAsync(PlayerCard playerCard, CancellationToken cancellationToken);
        Task<long> PlacePlayerCardToHandAsync(PlayerCard playerCard, long playerId, CancellationToken cancellationToken);
        Task<IEnumerable<long>> CreatePlayerCardsAsync(IEnumerable<PlayerCard> playerCards, CancellationToken cancellationToken);
        Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken);
        Task<IEnumerable<long>> CreateGameBoardCardsAsync(IEnumerable<GameBoardCard> gameBoardCards, CancellationToken cancellationToken);
        Task DeletePlayerCardAsync(long playerCardId, CancellationToken cancellationToken);
        Task DeleteAllPlayerCardAsync(long playerId, CancellationToken cancellationToken);
    }
}
