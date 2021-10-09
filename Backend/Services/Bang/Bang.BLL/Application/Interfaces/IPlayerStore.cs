using Bang.DAL.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IPlayerStore
    {
        Task<Player> GetPlayerAsync(long id, CancellationToken cancellationToken);
        Task<Player> GetPlayerByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<Player> GetOwnPlayerAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetTargetablePlayersAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersAliveByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken);
        Task<long> CreatePlayerAsync(Player player, CancellationToken cancellationToken);
        Task<long> DecrementPlayerHealthAsync(CancellationToken cancellationToken);
        Task<int> GetRemainingPlayerCountAsync(long gameBoardId, CancellationToken cancellationToken);
        Task SetPlayerPlacementAsync(long playerId, long gameBoardId, CancellationToken cancellationToken);
        Task DeletePlayerPlayedCardAsync(CancellationToken cancellationToken);
    }
}
