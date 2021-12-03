using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class CardQueryHandler :
        IRequestHandler<GetCardByTypeQuery, CardViewModel>,
        IRequestHandler<GetCardsQuery, IEnumerable<CardViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICardStore _cardStore;

        public CardQueryHandler(IMapper mapper, ICardStore cardStore)
        {
            _mapper = mapper;
            _cardStore = cardStore;
        }

        public async Task<CardViewModel> Handle(GetCardByTypeQuery request, CancellationToken cancellationToken)
        {
            var domain = await _cardStore.GetCardByTypeAsync(request.Type, cancellationToken);

            return _mapper.Map<CardViewModel>(domain);
        }

        public async Task<IEnumerable<CardViewModel>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _cardStore.GetCardsAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CardViewModel>>(domain);
        }
    }
}
