using Bang.DAL.Domain;
using Bang.DAL.Domain.Constants.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IPlayerStore
    {
        Task<Player> GetPlayerAsync(long id, CancellationToken cancellationToken);
        Task<Player> GetPlayerSimplifiedAsync(long id, CancellationToken cancellationToken);
        Task<Player> GetPlayerByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<Player> GetPlayerByUserIdSimplifiedAsync(string userId, CancellationToken cancellationToken);
        Task<Player> GetOwnPlayerAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetTargetablePlayersAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetTargetablePlayersByRangeAsync(long id, int range, CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersAliveByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersAliveByGameBoardAsync(CancellationToken cancellationToken);
        Task<Player> GetNextPlayerAliveByPlayerAsync(long playerId, CancellationToken cancellationToken);
        Task<long> CreatePlayerAsync(Player player, CancellationToken cancellationToken);
        Task<long> DecrementPlayerHealthAsync(CancellationToken cancellationToken);
        Task<long> IncrementPlayerHealthAsync(CancellationToken cancellationToken);
        Task<long> IncrementPlayerHealthAsync(long playerId, CancellationToken cancellationToken);
        Task<int> GetRemainingPlayerCountAsync(long gameBoardId, CancellationToken cancellationToken);
        Task SetPlayerPlacementAsync(long playerId, int placement, CancellationToken cancellationToken);
        Task DeletePlayerPlayedCardAsync(CancellationToken cancellationToken);
        Task DiscardCardAsync(long playerCardId, CancellationToken cancellationToken);
        Task AddPlayedCardAsync(CardType cardType, long playerId, CancellationToken cancellationToken);
        Task AddPlayedCardAsync(CardType cardType, CancellationToken cancellationToken);
        Task GainHealthForCardsAsync(IEnumerable<long> cards, CancellationToken cancellationToken);
    }
}
