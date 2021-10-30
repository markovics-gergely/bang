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
using Bang.DAL.Domain.Joins.PlayerCards;

namespace Bang.BLL.Infrastructure.Stores
{
    public class PlayerStore : IPlayerStore
    {
        private readonly BangDbContext _dbContext;
        private readonly IAccountStore _accountStore;
        private readonly ICardStore _cardStore;

        public PlayerStore(BangDbContext dbContext, IAccountStore accountStore, ICardStore cardStore)
        {
            _dbContext = dbContext;
            _accountStore = accountStore;
            _cardStore = cardStore;
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
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Player>> GetPlayersByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(p => p.GameBoardId == gameBoardId)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Player>> GetPlayersAliveByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(p => p.GameBoardId == gameBoardId && p.ActualHP > 0)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Player>> GetPlayersAliveByGameBoardAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            Player player = await GetPlayerByUserIdAsync(userId, cancellationToken);
            return await _dbContext.Players.Where(p => p.GameBoardId == player.GameBoardId && p.ActualHP > 0)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                .ToListAsync(cancellationToken);
        }

        public async Task<long> CreatePlayerAsync(Player player, CancellationToken cancellationToken)
        {
            await _dbContext.Players.AddAsync(player, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return player.Id;
        }

        public async Task<long> DecrementPlayerHealthAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            Player player = await GetPlayerByUserIdAsync(userId, cancellationToken);
            int hp = player.ActualHP;
            player.ActualHP = hp - 1;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return player.ActualHP;
        }

        public async Task<long> IncrementPlayerHealthAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            Player player = await GetPlayerByUserIdAsync(userId, cancellationToken);
            int hp = player.ActualHP;
            player.ActualHP = hp == player.MaxHP ? hp : hp + 1;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return player.ActualHP;
        }

        public async Task<long> IncrementPlayerHealthAsync(long playerId, CancellationToken cancellationToken)
        {
            Player player = await GetPlayerAsync(playerId, cancellationToken);
            int hp = player.ActualHP;
            player.ActualHP = hp == player.MaxHP ? hp : hp + 1;
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

        public async Task<IEnumerable<Player>> GetTargetablePlayersByRangeAsync(long id, int range, CancellationToken cancellationToken)
        {
            Player player = await GetPlayerAsync(id, cancellationToken);
            List<Player> players = (List<Player>)await GetPlayersAliveByGameBoardAsync(player.GameBoardId, cancellationToken);
            List<long> ids = players.Select(p => p.Id).ToList();

            int playerCount = players.Count;
            int playerIndex = players.IndexOf(player);

            ids.Remove(player.Id);
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

        public async Task SetPlayerPlacementAsync(long playerId, int placement, CancellationToken cancellationToken)
        {
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

        public async Task<Player> GetOwnPlayerAsync(CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            return await _dbContext.Players.Where(c => c.UserId == userId)
                .Include(p => p.HandPlayerCards).ThenInclude(c => c.Card)
                .Include(p => p.TablePlayerCards).ThenInclude(c => c.Card)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Player not found!");
        }

        public async Task DiscardCardAsync(long playerCardId, CancellationToken cancellationToken)
        {
            HandPlayerCard playerCard = (HandPlayerCard)await _cardStore.GetPlayerCardAsync(playerCardId, cancellationToken);
            await _cardStore.PlacePlayerCardToDiscardedAsync(playerCard, cancellationToken);
        }

        public async Task AddPlayedCardAsync(CardType cardType, long playerId, CancellationToken cancellationToken)
        {
            var player = await GetPlayerAsync(playerId, cancellationToken);
            player.PlayedCards.Add(cardType);
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.Players
                    .SingleOrDefaultAsync(p => p.Id == playerId) == null)
                    throw new EntityNotFoundException("Nem található a player");
                else throw;
            }
        }

        public async Task AddPlayedCardAsync(CardType cardType, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var player = await GetPlayerByUserIdAsync(userId, cancellationToken);
            player.PlayedCards.Add(cardType);
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.Players
                    .SingleOrDefaultAsync(p => p.Id == player.Id) == null)
                    throw new EntityNotFoundException("Nem található a player");
                else throw;
            }
        }

        public async Task<Player> GetNextPlayerAliveByPlayerAsync(long playerId, CancellationToken cancellationToken)
        {
            var player = await GetPlayerAsync(playerId, cancellationToken);
            var players = new List<Player>(await GetPlayersAliveByGameBoardAsync(player.GameBoardId, cancellationToken));
            int actualId = players.FindIndex(p => p.Id == playerId);
            return actualId == players.Count - 1 ? players[0] : players[actualId + 1];
        }
    }
}
