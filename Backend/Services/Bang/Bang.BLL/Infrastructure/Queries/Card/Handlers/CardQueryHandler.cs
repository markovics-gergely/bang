using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.Queries;
using Bang.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using System;
using System.Diagnostics;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class CardQueryHandler :
        IRequestHandler<GetCardByTypeQuery, CardViewModel>,
        IRequestHandler<GetCardsQuery, IEnumerable<CardViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICardStore _cardStore;
        private readonly IAccountStore _accountStore;

        public CardQueryHandler(IMapper mapper, ICardStore cardStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _cardStore = cardStore;
            _accountStore = accountStore;
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
