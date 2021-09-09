using Bang.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface IGameBoardStore
    {
        Task<GameBoard> GetGameBoardAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken);
    }
}
