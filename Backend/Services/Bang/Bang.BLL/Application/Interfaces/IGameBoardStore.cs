using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins.GameBoardCards;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IGameBoardStore
    {
        Task<GameBoard> GetGameBoardAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Card>> GetCardsOnTopAsync(long id, int count, CancellationToken cancellationToken);
        Task<Card> GetLastDiscardedCardAsync(long id, CancellationToken cancellationToken);
        Task<GameBoardCard> GetGameBoardCardByTypeAsync(long id, CardType type, string statusType, CancellationToken cancellationToken);
        Task<long> CreateGameBoardAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task ShuffleCardsAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task DeleteGameBoardCardByStatusAsync(long id, CardType type, string statusType, CancellationToken cancellationToken);
        Task<long> CreateGameBoardCardByStatusAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken);
    }
}
