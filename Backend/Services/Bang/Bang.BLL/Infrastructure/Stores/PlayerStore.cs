using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Bang.BLL.Infrastructure.Stores
{
    public class PlayerStore : IPlayerStore
    {
        private readonly BangDbContext _dbContext;

        public PlayerStore(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Player> GetPlayerAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(c => c.Id == id)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                //.Include(p => p.User)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Card not found!");
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Players
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                //.Include(p => p.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Player>> GetPlayersByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(p => p.GameBoardId == gameBoardId)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                //.Include(p => p.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<long> CreatePlayerAsync(Player player, CancellationToken cancellationToken)
        {
            await _dbContext.Players.AddAsync(player, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return player.Id;
        }

        public async Task<long> DecrementPlayerHealthAsync(long playerId, CancellationToken cancellationToken)
        {
            Player player = await GetPlayerAsync(playerId, cancellationToken);
            int hp = player.ActualHP;
            player.ActualHP = hp - 1;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return player.ActualHP;
        }

        public async Task<IEnumerable<Player>> GetTargetablePlayersAsync(long id, CancellationToken cancellationToken)
        {
            Player player = await GetPlayerAsync(id, cancellationToken);
            List<Player> players = (List<Player>)await GetPlayersByGameBoardAsync(player.GameBoardId, cancellationToken);
            List<long> ids = players.Select(p => p.Id).ToList();

            int centerIndex = ids.IndexOf(id);
            int count = ids.Count;
            centerIndex += count;
            int range = player.ShootingRange;

            ids.AddRange(ids); ids.AddRange(ids);
            List<long> filtered = ids.GetRange(centerIndex - range, 2 * range + 1);
            filtered.Remove(id);
            return players.Where(p => filtered.Contains(p.Id));
        }

        public async Task<long> GetRemainingPlayerCountAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            return (await GetPlayersByGameBoardAsync(gameBoardId, cancellationToken)).Count();
        }
    }
}
