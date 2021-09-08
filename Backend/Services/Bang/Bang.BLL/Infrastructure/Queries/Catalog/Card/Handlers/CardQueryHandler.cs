using AutoMapper;
using Bang.BLL.Application.Interfaces.Catalog;
using Bang.BLL.Infrastructure.Queries.Catalog.Card.Queries;
using Bang.BLL.Infrastructure.Queries.Catalog.Card.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Infrastructure.Queries.Catalog.Card.Handlers
{
    public class CardQueryHandler :
        IRequestHandler<GetCardQuery, CardViewModel>,
        IRequestHandler<GetActiveCardByTypeQuery, CardViewModel>,
        IRequestHandler<GetPassiveCardByTypeQuery, CardViewModel>,
        IRequestHandler<GetCardsQuery, IEnumerable<CardViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICardStore _cardStore;

        public CardQueryHandler(IMapper mapper, ICardStore cardStore)
        {
            _mapper = mapper;
            _cardStore = cardStore;
        }

        public async Task<CardViewModel> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            var domain = await _cardStore.GetCardAsync(request.Id, cancellationToken);

            return _mapper.Map<CardViewModel>(domain);
        }

        public async Task<CardViewModel> Handle(GetActiveCardByTypeQuery request, CancellationToken cancellationToken)
        {
            var domain = await _cardStore.GetActiveCardAsync(request.Type, cancellationToken);

            return _mapper.Map<CardViewModel>(domain);
        }

        public async Task<CardViewModel> Handle(GetPassiveCardByTypeQuery request, CancellationToken cancellationToken)
        {
            var domain = await _cardStore.GetPassiveCardAsync(request.Type, cancellationToken);

            return _mapper.Map<CardViewModel>(domain);
        }

        public async Task<IEnumerable<CardViewModel>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _cardStore.GetCardsAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CardViewModel>>(domain);
        }
    }
}
