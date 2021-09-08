using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Interfaces.Catalog
{
    public interface ICardStore
    {
        Task<Card> GetCardAsync(long id, CancellationToken cancellationToken);
        Task<ActiveCard> GetActiveCardAsync(ActiveCardType type, CancellationToken cancellationToken);
        Task<PassiveCard> GetPassiveCardAsync(PassiveCardType type, CancellationToken cancellationToken);
        Task<IEnumerable<Card>> GetCardsAsync(CancellationToken cancellationToken);
    }
}
