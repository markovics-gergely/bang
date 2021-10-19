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
        Task<GameBoard> GetGameBoardByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Card>> GetCardsOnTopAsync(int count, CancellationToken cancellationToken);
        Task<IEnumerable<DrawableGameBoardCard>> GetGameBoardCardsOnTopAsync(int count, CancellationToken cancellationToken);
        Task<IEnumerable<DrawableGameBoardCard>> GetDrawableGameBoardCardsOnTopAsync(int count, CancellationToken cancellationToken);
        Task<Card> GetLastDiscardedCardAsync(CancellationToken cancellationToken);
        Task<DiscardedGameBoardCard> GetLastDiscardedGameBoardCardAsync(CancellationToken cancellationToken);
        Task<GameBoardCard> GetGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task<long> CreateGameBoardAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task ShuffleCardsAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task<DiscardedGameBoardCard> DiscardFromDrawableGameBoardCardAsync(CancellationToken cancellationToken);
        Task DeleteGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task DeleteAllGameBoardCardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken);
        Task SetGameBoardEndAsync(CancellationToken cancellationToken);
        Task SetGameBoardActualPlayerAsync(long playerId, CancellationToken cancellationToken);
        Task SetGameBoardTargetedPlayerAsync(long? playerId, CancellationToken cancellationToken);
        Task SetGameBoardPhaseAsync(PhaseEnum phaseEnum, CancellationToken cancellationToken);
        Task SetGameBoardTargetReasonAsync(TargetReason? targetReason, CancellationToken cancellationToken);
        Task EndGameBoardTurnAsync(CancellationToken cancellationToken);
        Task PlayCardAsync(long playerCardId, CancellationToken cancellationToken);
        Task PlayCardAsync(long playerCardId, long targetPlayerCardId, bool isTargetPlayer, CancellationToken cancellationToken);
        Task<long> DrawGameBoardCardAsync(long gameBoardCardId, long playerId, CancellationToken cancellationToken);
        Task DrawGameBoardCardsFromTopAsync(int count, long playerId, CancellationToken cancellationToken);
        Task DrawGameBoardCardsFromTopAsync(int count, CancellationToken cancellationToken);
        Task DrawGameBoardCardsToScatteredAsync(int count, CancellationToken cancellationToken);
        Task DrawGameBoardCardsToScatteredByPlayersAliveAsync(CancellationToken cancellationToken);
    }
}
