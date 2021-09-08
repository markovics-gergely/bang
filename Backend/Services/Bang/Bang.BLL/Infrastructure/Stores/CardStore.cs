using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces.Catalog;
using Bang.DAL;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants;
using Bang.DAL.Domain.Constants.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Stores
{
    public class CardStore : ICardStore
    {
        private readonly BangDbContext _dbContext;

        public CardStore(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActiveCard> GetActiveCardAsync(ActiveCardType type, CancellationToken cancellationToken)
        {
            return (ActiveCard)(await _dbContext.Cards
                .Where(c => c.CardEffectType == CardConstants.PassiveCard &&
                            ((ActiveCard)c).CardType == type).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Card not found!"));
        }

        public async Task<Card> GetCardAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Cards.Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Card not found!");
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Cards.ToListAsync(cancellationToken);
        }

        public async Task<PassiveCard> GetPassiveCardAsync(PassiveCardType type, CancellationToken cancellationToken)
        {
            return (PassiveCard)(await _dbContext.Cards
                .Where(c => c.CardEffectType == CardConstants.PassiveCard && 
                            ((PassiveCard)c).CardType == type).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Card not found!"));
        }
    }
}
