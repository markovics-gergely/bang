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

namespace Bang.BLL.Infrastructure.Stores
{
    public class GameBoardStore : IGameBoardStore
    {
        private readonly BangDbContext _dbContext;

        public GameBoardStore(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> CreateGameBoardAsync(GameBoard gameBoard, CancellationToken cancellationToken)
        {
            await _dbContext.GameBoards.AddAsync(gameBoard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return gameBoard.Id;
        }

        public async Task<long> CreateGameBoardCardByStatusAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken)
        {
            await _dbContext.GameBoardCards.AddAsync(gameBoardCard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return gameBoardCard.Id;
        }

        public async Task DeleteGameBoardCardByStatusAsync(long id, CardType type, string statusType, CancellationToken cancellationToken)
        {
            GameBoardCard deletable = await GetGameBoardCardByTypeAsync(id, type, statusType, cancellationToken);
            _dbContext.GameBoardCards.Remove(deletable);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.GameBoardCards.FirstOrDefaultAsync(p => p.Id == id, cancellationToken) == null)
                    throw new EntityNotFoundException("Character not found!");
                else throw;
            }
        }

        public async Task<IEnumerable<Card>> GetCardsOnTopAsync(long id, int count, CancellationToken cancellationToken)
        {
            GameBoard gameBoard = await GetGameBoardAsync(id, cancellationToken);

            List<Card> drawables = new(gameBoard.DrawableGameBoardCards.Select(d => d.Card));
            return drawables.GetRange(drawables.Count - 1 - count, drawables.Count - 1);
        }

        public async Task<GameBoard> GetGameBoardAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.GameBoards.Where(c => c.Id == id)
                .Include(g => g.Players).ThenInclude(p => p.PlayerCards)
                .Include(g => g.DrawableGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.DiscardedGameBoardCards).ThenInclude(d => d.Card)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("GameBoard not found!");
        }

        public async Task<GameBoardCard> GetGameBoardCardByTypeAsync(long id, CardType type, string statusType, CancellationToken cancellationToken)
        {
            return await _dbContext.GameBoardCards.Where(c => c.GameBoardId == id && c.Card.CardType == type && c.StatusType == statusType)
                .Include(g => g.Card).FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("GameBoardCard not found!");
        }

        public async Task<IEnumerable<GameBoard>> GetGameBoardsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.GameBoards
                .Include(g => g.Players).ThenInclude(p => p.PlayerCards)
                .Include(g => g.DrawableGameBoardCards).ThenInclude(d => d.Card)
                .Include(g => g.DiscardedGameBoardCards).ThenInclude(d => d.Card)
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
            List<Card> deletables = (List<Card>)gameBoard.DiscardedGameBoardCards.Select(d => d.Card);
            List<DrawableGameBoardCard> drawables = new List<DrawableGameBoardCard>();
            foreach (Card card in deletables)
            {
                DrawableGameBoardCard drawable = new DrawableGameBoardCard()
                {
                    CardId = card.Id,
                    GameBoardId = gameBoard.Id,
                    StatusType = GameBoardCardConstants.DrawableCard
                };
                drawables.Add(drawable);
                await DeleteGameBoardCardByStatusAsync(gameBoard.Id, card.CardType, 
                        GameBoardCardConstants.DiscardedCard, cancellationToken);
            }
            var rnd = new Random();
            drawables = drawables.OrderBy(d => rnd.Next()).ToList();
            foreach (DrawableGameBoardCard drawable in drawables)
            {
                await CreateGameBoardCardByStatusAsync(drawable, cancellationToken);
            }
        }
    }
}
