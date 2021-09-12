using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;

namespace Bang.BLL.Infrastructure.Stores
{
    public class CardStore : ICardStore
    {
        private readonly BangDbContext _dbContext;

        public CardStore(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Card> GetCardByTypeAsync(CardType type, CancellationToken cancellationToken)
        {
            return await _dbContext.Cards.Where(c => c.CardType == type).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Card not found!");
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Cards.ToListAsync(cancellationToken);
        }

        public async Task PlayCardAsync(CardType type, long playerId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<long> CreatePlayerCardAsync(PlayerCard playerCard, CancellationToken cancellationToken)
        {
            await _dbContext.PlayerCards.AddAsync(playerCard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return playerCard.Id;
        }

        public async Task<IEnumerable<long>> CreatePlayerCardsAsync(IEnumerable<PlayerCard> playerCards, CancellationToken cancellationToken)
        {
            List<long> playerCardIds = new List<long>();

            foreach (PlayerCard playerCard in playerCards)
            { 
                await _dbContext.PlayerCards.AddAsync(playerCard, cancellationToken);
                playerCardIds.Add(playerCard.Id);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return playerCardIds;
        }

        public async Task<long> CreateGameBoardCardAsync(GameBoardCard gameBoardCard, CancellationToken cancellationToken)
        {
            await _dbContext.GameBoardCards.AddAsync(gameBoardCard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return gameBoardCard.Id;
        }

        public async Task<IEnumerable<long>> CreateGameBoardCardsAsync(IEnumerable<GameBoardCard> gameBoardCards, CancellationToken cancellationToken)
        {
            List<long> gameBoardCardIds = new List<long>();

            foreach (GameBoardCard gameBoardCard in gameBoardCards)
            {
                await _dbContext.GameBoardCards.AddAsync(gameBoardCard, cancellationToken);
                gameBoardCardIds.Add(gameBoardCard.Id);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return gameBoardCardIds;
        }
    }
}
