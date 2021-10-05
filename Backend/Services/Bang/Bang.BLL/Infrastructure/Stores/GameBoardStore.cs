using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Constants;
using MediatR;
using System;
using Bang.DAL.Domain.Joins.PlayerCards;

namespace Bang.BLL.Infrastructure.Stores
{
    public class GameBoardStore : IGameBoardStore
    {
        private readonly BangDbContext _dbContext;
        private readonly IAccountStore _accountStore;

        public GameBoardStore(BangDbContext dbContext, IAccountStore accountStore)
        {
            _dbContext = dbContext;
            _accountStore = accountStore;
        }

        public async Task<long> CreateGameBoardAsync(GameBoard gameBoard, CancellationToken cancellationToken)
        {
            await _dbContext.GameBoards.AddAsync(gameBoard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return gameBoard.Id;
        }

        public async Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken)
        {
            await _dbContext.GameBoardCards.AddAsync(gameBoardCard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return gameBoardCard.Id;
        }

        public async Task DeleteGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken)
        {
            GameBoardCard deletable = await GetGameBoardCardAsync(gameBoardCardId, cancellationToken);
            _dbContext.GameBoardCards.Remove(deletable);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoardCards.FirstOrDefaultAsync(p => p.Id == gameBoardCardId, cancellationToken) == null)
                    throw new EntityNotFoundException("GameBoardCard not found!");
                else throw;
            }
        }

        public async Task DeleteAllGameBoardCardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            List<GameBoardCard> deletables = await _dbContext.GameBoardCards.Where(c => c.GameBoardId == gameBoardId)
                .ToListAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoardCard not found!");
            foreach (GameBoardCard gameBoardCard in deletables)
            {
                _dbContext.GameBoardCards.Remove(gameBoardCard);
            }
            
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoardCards.Where(p => p.GameBoardId == gameBoardId).ToListAsync(cancellationToken) == null)
                    throw new EntityNotFoundException("GameBoardCard not found!");
                else throw;
            }
        }

        public async Task<DiscardedGameBoardCard> DiscardFromDrawableGameBoardCardAsync(long id, CancellationToken cancellationToken)
        {
            var domain = (await GetDrawableGameBoardCardsOnTopAsync(id, 1, cancellationToken)).FirstOrDefault();
            var discarded = new DiscardedGameBoardCard()
            {
                CardId = domain.Card.Id,
                GameBoardId = id, 
                CardColorType = domain.CardColorType,
                FrenchNumber = domain.FrenchNumber
            };
            await DeleteGameBoardCardAsync(domain.Id, cancellationToken);
            long newId = await CreateGameBoardCardAsync(discarded, cancellationToken);
            return (DiscardedGameBoardCard)await GetGameBoardCardAsync(newId, cancellationToken);
        }

        public async Task<IEnumerable<Card>> GetCardsOnTopAsync(long id, int count, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(id, cancellationToken);

            List<Card> drawables = new(gameBoard.DrawableGameBoardCards.Select(d => d.Card));
            return drawables.GetRange(drawables.Count - 1 - count, drawables.Count - 1);
        }

        public async Task<IEnumerable<DrawableGameBoardCard>> GetDrawableGameBoardCardsOnTopAsync(long id, int count, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(id, cancellationToken);

            List<DrawableGameBoardCard> drawables = (List<DrawableGameBoardCard>)gameBoard.DrawableGameBoardCards;
            if(drawables.Count < count)
            {
                count = drawables.Count;
                if (count == 0) return new List<DrawableGameBoardCard>();
            }
            return drawables.GetRange(drawables.Count - count, count);
        }

        public async Task<GameBoard> GetGameBoardAsync(long id, CancellationToken cancellationToken)
        {
            var aid = await _accountStore.GetActualAccountId();
            Console.WriteLine(aid);
            return await _dbContext.GameBoards.Where(c => c.Id == id)
                .Include(g => g.Players).ThenInclude(p => p.HandPlayerCards).ThenInclude(c => (c as HandPlayerCard).Card)
                .Include(g => g.Players).ThenInclude(p => p.TablePlayerCards).ThenInclude(c => (c as TablePlayerCard).Card)
                .Include(g => g.DrawableGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.DiscardedGameBoardCards).ThenInclude(d => d.Card)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoard not found!");
        }

        public async Task<GameBoardCard> GetGameBoardCardAsync(long gameBoardCardId, CancellationToken cancellationToken)
        {
            return await _dbContext.GameBoardCards.Where(c => c.Id == gameBoardCardId)
                .Include(g => g.Card).FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("GameBoardCard not found!");
        }

        public async Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.GameBoards
                .Include(g => g.Players).ThenInclude(p => p.HandPlayerCards)
                .ToListAsync(cancellationToken);
        }

        public async Task<Card> GetLastDiscardedCardAsync(long id, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(id, cancellationToken);

            if(gameBoard.DiscardedGameBoardCards.Count == 0) 
                throw new EntityNotFoundException("Card not found!");
            return gameBoard.DiscardedGameBoardCards.Last().Card;
        }

        public async Task ShuffleCardsAsync(GameBoard gameBoard, CancellationToken cancellationToken)
        {
            List<DiscardedGameBoardCard> deletables = (List<DiscardedGameBoardCard>)gameBoard.DiscardedGameBoardCards;
            List<DrawableGameBoardCard> drawables = new List<DrawableGameBoardCard>();
            foreach (DiscardedGameBoardCard gameBoardCard in deletables)
            {
                DrawableGameBoardCard drawable = new DrawableGameBoardCard()
                {
                    CardId = gameBoardCard.Card.Id,
                    GameBoardId = gameBoard.Id,
                    StatusType = GameBoardCardConstants.DrawableCard,
                    CardColorType = gameBoardCard.CardColorType, 
                    FrenchNumber = gameBoardCard.FrenchNumber
                };
                drawables.Add(drawable);
                await DeleteGameBoardCardAsync(gameBoard.Id, cancellationToken);
            }
            var rnd = new Random();
            drawables = drawables.OrderBy(d => rnd.Next()).ToList();
            foreach (DrawableGameBoardCard drawable in drawables)
            {
                await CreateGameBoardCardAsync(drawable, cancellationToken);
            }
        }

        public async Task<DiscardedGameBoardCard> GetLastDiscardedGameBoardCardAsync(long id, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(id, cancellationToken);

            if (gameBoard.DiscardedGameBoardCards.Count == 0)
                throw new EntityNotFoundException("Card not found!");
            return gameBoard.DiscardedGameBoardCards.Last();
        }

        public async Task SetGameBoardEndAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(gameBoardId, cancellationToken);
            gameBoard.IsOver = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            await DeleteAllGameBoardCardAsync(gameBoardId, cancellationToken);
        }

        public async Task<GameBoard> GetGameBoardByUserAsync(string userId, CancellationToken cancellationToken)
        {
            Player player = await _dbContext.Players.Where(c => c.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Player not found!");
            return await _dbContext.GameBoards.Where(g => g.Players.Contains(player))
                .Include(g => g.Players).ThenInclude(p => p.HandPlayerCards).ThenInclude(c => (c as HandPlayerCard).Card)
                .Include(g => g.Players).ThenInclude(p => p.TablePlayerCards).ThenInclude(c => (c as TablePlayerCard).Card)
                .Include(g => g.DrawableGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.DiscardedGameBoardCards).ThenInclude(d => d.Card)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoard not found!");
        }

        public async Task SetGameBoardActualPlayerAsync(long gameBoardId, long playerId, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(gameBoardId, cancellationToken);
            gameBoard.TargetedPlayerId = playerId;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SetGameBoardTargetedPlayerAsync(long gameBoardId, long? playerId, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(gameBoardId, cancellationToken);
            gameBoard.ActualPlayerId = playerId;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
