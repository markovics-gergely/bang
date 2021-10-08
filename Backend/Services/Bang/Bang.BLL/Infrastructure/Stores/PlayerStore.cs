using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Infrastructure.Stores
{
    public class PlayerStore : IPlayerStore
    {
        private readonly BangDbContext _dbContext;
        private readonly IAccountStore _accountStore;

        public PlayerStore(BangDbContext dbContext, IAccountStore accountStore)
        {
            _dbContext = dbContext;
            _accountStore = accountStore;
        }

        public async Task<Player> GetPlayerAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(c => c.Id == id)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Player not found!");
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

        public async Task<IEnumerable<Player>> GetPlayersAliveByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(p => p.GameBoardId == gameBoardId && p.ActualHP > 0)
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
            List<Player> players = (List<Player>)await GetPlayersAliveByGameBoardAsync(player.GameBoardId, cancellationToken);
            List<long> ids = players.Select(p => p.Id).ToList();

            int playerCount = players.Count;
            int playerIndex = players.IndexOf(player);

            ids.Remove(player.Id);
            int range = player.ShootingRange;
            if (player.TablePlayerCards.Any(p => p.Card.CardType == CardType.Scope)) range++;
            foreach (Player p in players)
            {
                int pIndex = players.IndexOf(p);
                int distance = (new List<int>() { Math.Abs(pIndex - playerIndex), 
                                                  Math.Abs(pIndex + playerCount - playerIndex),
                                                  Math.Abs(pIndex - playerCount - playerIndex) }).Min();
                if (p.CharacterType == CharacterType.PaulRegret) distance++;
                if (p.TablePlayerCards.Any(p => p.Card.CardType == CardType.Mustang)) distance++;
                if (distance > range) ids.Remove(p.Id);
            }
            return players.Where(p => ids.Contains(p.Id));
        }

        public async Task<int> GetRemainingPlayerCountAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            return (await GetPlayersAliveByGameBoardAsync(gameBoardId, cancellationToken)).Count();
        }

        public async Task SetPlayerPlacementAsync(long playerId, long gameBoardId, CancellationToken cancellationToken)
        {
            int placement = await GetRemainingPlayerCountAsync(gameBoardId, cancellationToken) + 1;
            Player player = await GetPlayerAsync(playerId, cancellationToken);
            player.Placement = placement;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePlayerPlayedCardAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var player = await GetPlayerByUserIdAsync(userId, cancellationToken);
            player.PlayedCards.Clear();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Player> GetPlayerByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(c => c.UserId == userId)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Player not found!");
        }
    }
}
