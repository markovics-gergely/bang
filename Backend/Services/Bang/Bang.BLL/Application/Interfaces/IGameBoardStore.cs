using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IGameBoardStore
    {
        Task<GameBoard> GetGameBoardAsync(long id, CancellationToken cancellationToken);
        Task<GameBoard> GetGameBoardSimplifiedAsync(long id, CancellationToken cancellationToken);
        Task<GameBoard> GetGameBoardByUserAsync(string userId, CancellationToken cancellationToken);
        Task<GameBoard> GetGameBoardByUserSimplifiedAsync(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken);
        Task<string> GetGameBoardOwnerByUserAsync(string userId, CancellationToken cancellationToken);
        Task<TargetReason?> GetGameBoardTargetReasonAsync(long id, CancellationToken cancellationToken);

        Task<IEnumerable<Card>> GetCardsOnTopAsync(int count, CancellationToken cancellationToken);
        Task<IEnumerable<DrawableGameBoardCard>> GetGameBoardCardsOnTopAsync(int count, CancellationToken cancellationToken);
        Task<IEnumerable<DrawableGameBoardCard>> GetDrawableGameBoardCardsOnTopAsync(int count, CancellationToken cancellationToken);
        Task<Card> GetLastDiscardedCardAsync(CancellationToken cancellationToken);
        Task<DiscardedGameBoardCard> GetLastDiscardedGameBoardCardAsync(CancellationToken cancellationToken);
        Task<GameBoardCard> GetGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task<GameBoardCard> GetGameBoardCardSimplifiedAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task<long> CreateGameBoardAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task ShuffleCardsAsync(GameBoard gameBoard, CancellationToken cancellationToken);
        Task<DiscardedGameBoardCard> DiscardFromDrawableGameBoardCardAsync(CancellationToken cancellationToken);
        Task SetDiscardInDiscardingPhaseResultAsync(DiscardedGameBoardCard gameBoardCard, GameBoard gameBoard, CancellationToken cancellationToken);
        Task SetDiscardInDiscardingPhaseResultAsync(IEnumerable<DiscardedGameBoardCard> gameBoardCards, GameBoard gameBoard, CancellationToken cancellationToken);
        Task DeleteGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task DeleteAllGameBoardCardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken);
        Task SetGameBoardDrawnFromMiddleAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task SetGameBoardEndAsync(CancellationToken cancellationToken);
        Task SetGameBoardEndAsync(long id, CancellationToken cancellationToken);
        Task SetGameBoardActualPlayerAsync(long playerId, CancellationToken cancellationToken);
        Task SetGameBoardTargetedPlayerAsync(long? playerId, CancellationToken cancellationToken);
        Task SetGameBoardTargetReasonAsync(TargetReason? targetReason, CancellationToken cancellationToken);
        Task SetGameBoardTargetedPlayerAndReasonAsync(long? playerId, TargetReason? targetReason, CancellationToken cancellationToken);
        Task SetGameBoardNextTargetedAsync(long? playerId, long gameBoardId, CancellationToken cancellationToken);
        Task SetGameBoardPhaseAsync(PhaseEnum phaseEnum, CancellationToken cancellationToken);
        Task EndGameBoardTurnAsync(CancellationToken cancellationToken);
        Task SetGameBoardLastTargetedPlayerAsync(long? lastTargetedPlayerId, CancellationToken cancellationToken);
        Task SetGameBoardLobbyOwnerIdAsync(string? lobbyOwnerId, CancellationToken cancellationToken);
        Task PlayCardAsync(long playerCardId, CancellationToken cancellationToken);
        Task PlayCardAsync(long playerCardId, long targetPlayerCardId, bool isTargetPlayer, CancellationToken cancellationToken);
        Task<long> DrawGameBoardCardAsync(long gameBoardCardId, long playerId, CancellationToken cancellationToken);
        Task<long> DrawGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken);
        Task DrawGameBoardCardsFromTopAsync(int count, long playerId, CancellationToken cancellationToken);
        Task DrawGameBoardCardsFromTopAsync(int count, CancellationToken cancellationToken);
        Task DrawGameBoardCardsToScatteredAsync(int count, CancellationToken cancellationToken);
        Task DrawGameBoardCardsToScatteredByPlayersAliveAsync(CancellationToken cancellationToken);
        Task<bool> CalculatePlayerPlacementAsync(long deadPlayerId, CancellationToken cancellationToken);
        Task DeleteGameBoardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task UseBarrelAsync(long gameBoardId, CancellationToken cancellationToken);
    }
}
