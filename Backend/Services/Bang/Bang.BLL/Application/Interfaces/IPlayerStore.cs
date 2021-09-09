using Bang.DAL.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IPlayerStore
    {
        Task<Player> GetPlayerAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Player>> GetPlayersByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken);
    }
}
