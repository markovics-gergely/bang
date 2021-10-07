using Bang.BLL.Infrastructure.Queries.ViewModels;
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
        Task<GameBoard> GetGameBoardByUserAsync(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Card>> GetCardsOnTopAsync(long id, int count, CancellationToken cancellationToken);
        Task<IEnumerable<DrawableGameBoardCard>> GetDrawableGameBoardCardsOnTopAsync(long id, int count, CancellationToken cancellationToken);
        Task<Card> GetLastDiscardedCardAsync(long id, CancellationToken cancellationToken);
        Task<DiscardedGameBoardCard> GetLastDiscardedGameBoardCardAsync(long id, CancellationToken cancellationToken);
        Task<GameBoardCard> GetGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task<long> CreateGameBoardAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task ShuffleCardsAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task<DiscardedGameBoardCard> DiscardFromDrawableGameBoardCardAsync(long id, CancellationToken cancellationToken);
        Task DeleteGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task DeleteAllGameBoardCardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken);
        Task SetGameBoardEndAsync(long gameBoardId, CancellationToken cancellationToken);
        Task SetGameBoardActualPlayerAsync(long gameBoardId, long playerId, CancellationToken cancellationToken);
        Task SetGameBoardTargetedPlayerAsync(long gameBoardId, long? playerId, CancellationToken cancellationToken);
        Task SetGameBoardPhaseAsync(PhaseEnum phaseEnum, CancellationToken cancellationToken);
        Task SetGameBoardTargetReasonAsync(TargetReason? targetReason, CancellationToken cancellationToken);
        Task EndGameBoardTurnAsync(CancellationToken cancellationToken);
    }
}
