using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface ICardStore
    {
        Task<Card> GetCardAsync(long id, CancellationToken cancellationToken);
        Task<Card> GetCardByTypeAsync(CardType type, CancellationToken cancellationToken);
        Task<IEnumerable<Card>> GetCardsAsync(CancellationToken cancellationToken);

        public Task PlayCardAsync(CardType type, long playerId, CancellationToken cancellationToken);
        public Task<long> CreatePlayerCardAsync(PlayerCard playerCard, CancellationToken cancellationToken);
        public Task<IEnumerable<long>> CreatePlayerCardsAsync(IEnumerable<PlayerCard> playerCards, CancellationToken cancellationToken);
        public Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken);
        public Task<IEnumerable<long>> CreateGameBoardCardsAsync(IEnumerable<GameBoardCard> gameBoardCards, CancellationToken cancellationToken);
    }
}
