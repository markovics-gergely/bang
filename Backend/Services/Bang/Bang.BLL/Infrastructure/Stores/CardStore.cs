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
using Bang.DAL.Domain.Joins.PlayerCards;
using Bang.BLL.Application.Effects.Cards;

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

        public async Task PlayCardAsync(long playerCardId, CancellationToken cancellationToken)
        {
            PlayerCard playerCard = await GetPlayerCardAsync(playerCardId, cancellationToken);
            Dictionary<CardType, CardEffect> cardEffectMap = CardEffectHandler.Instance.CardEffectMap;
            await cardEffectMap[playerCard.Card.CardType].Execute(new CardEffectQuery(playerCard.Player));
            if(playerCard.Card.CardEffectType == CardConstants.PassiveCard)
            {
                await PlaceHandPlayerCardToTableAsync((HandPlayerCard)playerCard, cancellationToken);
            } else if (playerCard.Card.CardEffectType == CardConstants.ActiveCard)
            {
                await PlaceHandPlayerCardToDiscardedAsync((HandPlayerCard)playerCard, cancellationToken);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
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

        public async Task<long> PlaceHandPlayerCardToTableAsync(HandPlayerCard playerCard, CancellationToken cancellationToken)
        {
            TablePlayerCard tablePlayerCard = new TablePlayerCard()
            {
                CardId = playerCard.CardId,
                PlayerId = playerCard.PlayerId,
                CardColorType = playerCard.CardColorType,
                FrenchNumber = playerCard.FrenchNumber
            };
            await DeletePlayerCardAsync(playerCard.Id, cancellationToken);
            return await CreatePlayerCardAsync(tablePlayerCard, cancellationToken);
        }

        public async Task<long> PlaceHandPlayerCardToDiscardedAsync(HandPlayerCard playerCard, CancellationToken cancellationToken)
        {
            DiscardedGameBoardCard discardedGameBoardCard = new DiscardedGameBoardCard()
            {
                CardId = playerCard.CardId,
                GameBoardId = playerCard.Player.GameBoardId,
                CardColorType = playerCard.CardColorType,
                FrenchNumber = playerCard.FrenchNumber
            };
            await DeletePlayerCardAsync(playerCard.Id, cancellationToken);
            return await CreateGameBoardCardAsync(discardedGameBoardCard, cancellationToken);
        }

        public async Task DeletePlayerCardAsync(long playerCardId, CancellationToken cancellationToken)
        {
            PlayerCard deletable = await GetPlayerCardAsync(playerCardId, cancellationToken);
            _dbContext.PlayerCards.Remove(deletable);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.PlayerCards.FirstOrDefaultAsync(p => p.Id == playerCardId, cancellationToken) == null)
                    throw new EntityNotFoundException("PlayerCard not found!");
                else throw;
            }
        }

        public async Task<PlayerCard> GetPlayerCardAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.PlayerCards.Where(c => c.Id == id)
                .Include(p => p.Card)
                .Include(p => (p as HandPlayerCard).Player)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("PlayerCard not found!");
        }

        public async Task DeleteAllPlayerCardAsync(long playerId, CancellationToken cancellationToken)
        {
            List<PlayerCard> playerCards = (List<PlayerCard>)await GetPlayerCardsAsync(playerId, cancellationToken);
            foreach (PlayerCard playerCard in playerCards)
            {
                await DeletePlayerCardAsync(playerCard.Id, cancellationToken);
            }
        }

        public async Task<IEnumerable<PlayerCard>> GetPlayerCardsAsync(long playerId, CancellationToken cancellationToken)
        {
            return await _dbContext.PlayerCards.Where(c => c.PlayerId == playerId).ToListAsync(cancellationToken)
                ?? throw new EntityNotFoundException("PlayerCard not found!");
        }
    }
}
