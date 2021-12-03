using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Bang.DAL.Domain.Joins;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Joins.PlayerCards;
using Bang.DAL.Domain;

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

        public async Task<long> CreatePlayerCardAsync(PlayerCard playerCard, CancellationToken cancellationToken)
        {
            await _dbContext.PlayerCards.AddAsync(playerCard, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return playerCard.Id;
        }

        public async Task<IEnumerable<long>> CreatePlayerCardsAsync(IEnumerable<PlayerCard> playerCards, CancellationToken cancellationToken)
        {
            var playerCardIds = new List<long>();

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
            TablePlayerCard deletable = null;
            if (playerCard.Card.CardType.IsWeapon())
            {
                deletable = playerCard.Player.TablePlayerCards.FirstOrDefault(c => c.Card.CardType.IsWeapon());
            }
            if (playerCard.Card.CardType.IsRangeModifier())
            {
                deletable = playerCard.Player.TablePlayerCards.FirstOrDefault(c => c.Card.CardType.IsRangeModifier());
            }
            if (playerCard.Card.CardType == CardType.Dynamite)
            {
                deletable = playerCard.Player.TablePlayerCards.FirstOrDefault(c => c.Card.CardType == CardType.Dynamite);
            }
            if (playerCard.Card.CardType == CardType.Jail)
            {
                deletable = playerCard.Player.TablePlayerCards.FirstOrDefault(c => c.Card.CardType == CardType.Jail);
            }
            if (deletable != null)
            {
                DiscardedGameBoardCard discarded = new DiscardedGameBoardCard()
                {
                    CardId = deletable.CardId,
                    GameBoardId = deletable.Player.GameBoardId,
                    CardColorType = deletable.CardColorType,
                    FrenchNumber = deletable.FrenchNumber
                };
                await DeletePlayerCardAsync(deletable.Id, cancellationToken);
                await CreateGameBoardCardAsync(discarded, cancellationToken);
            }
            await DeletePlayerCardAsync(playerCard.Id, cancellationToken);
            return await CreatePlayerCardAsync(tablePlayerCard, cancellationToken);
        }

        public async Task<long> PlacePlayerCardToDiscardedAsync(PlayerCard playerCard, CancellationToken cancellationToken)
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

        public async Task<long> PlacePlayerCardToHandAsync(PlayerCard playerCard, long playerId, CancellationToken cancellationToken)
        {
            HandPlayerCard handPlayerCard = new HandPlayerCard()
            {
                CardId = playerCard.CardId,
                PlayerId = playerId,
                CardColorType = playerCard.CardColorType,
                FrenchNumber = playerCard.FrenchNumber
            };
            await DeletePlayerCardAsync(playerCard.Id, cancellationToken);
            return await CreatePlayerCardAsync(handPlayerCard, cancellationToken);
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
            var playerCard = await _dbContext.PlayerCards.Where(c => c.Id == id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("PlayerCard not found!");
            if (playerCard is HandPlayerCard)
            {
                return await _dbContext.PlayerCards.Where(c => c.Id == id)
                    .Include(p => ((HandPlayerCard)p).Card)
                    .Include(p => ((HandPlayerCard)p).Player)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            else
            {
                return await _dbContext.PlayerCards.Where(c => c.Id == id)
                    .Include(p => ((TablePlayerCard)p).Card)
                    .Include(p => ((TablePlayerCard)p).Player)
                    .FirstOrDefaultAsync(cancellationToken);
            }
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

        public async Task<long> PlacePlayerCardToAnotherTableAsync(PlayerCard playerCard, Player targetPlayer, CancellationToken cancellationToken)
        {
            TablePlayerCard tablePlayerCard = new TablePlayerCard()
            {
                CardId = playerCard.CardId,
                PlayerId = targetPlayer.Id,
                CardColorType = playerCard.CardColorType,
                FrenchNumber = playerCard.FrenchNumber
            };
            TablePlayerCard deletable = null;
            if (playerCard.Card.CardType.IsWeapon())
            {
                deletable = targetPlayer.TablePlayerCards.FirstOrDefault(c => c.Card.CardType.IsWeapon());
            }
            if (playerCard.Card.CardType.IsRangeModifier())
            {
                deletable = targetPlayer.TablePlayerCards.FirstOrDefault(c => c.Card.CardType.IsRangeModifier());
            }
            if (deletable != null)
            {
                DiscardedGameBoardCard discarded = new DiscardedGameBoardCard()
                {
                    CardId = deletable.CardId,
                    GameBoardId = deletable.Player.GameBoardId,
                    CardColorType = deletable.CardColorType,
                    FrenchNumber = deletable.FrenchNumber
                };
                await DeletePlayerCardAsync(deletable.Id, cancellationToken);
                await CreateGameBoardCardAsync(discarded, cancellationToken);
            }
            await DeletePlayerCardAsync(playerCard.Id, cancellationToken);
            return await CreatePlayerCardAsync(tablePlayerCard, cancellationToken);
        }
    }
}
