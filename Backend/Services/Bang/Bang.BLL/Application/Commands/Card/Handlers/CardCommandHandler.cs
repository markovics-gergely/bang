﻿using Bang.BLL.Application.Interfaces;

using System.Threading.Tasks;
using Bang.BLL.Application.Effects.Cards;
using Bang.BLL.Application.Commands.Commands;
using Bang.DAL.Domain.Joins;

using System.Threading;

using AutoMapper;
using MediatR;

namespace Bang.BLL.Application.Commands.Handlers
{
    public class CardCommandHandler :
        IRequestHandler<CreateGameBoardCardCommand, long>,
        IRequestHandler<CreatePlayerCardCommand, long>,
        IRequestHandler<PlayCardCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICardStore _cardStore;

        public CardCommandHandler(IMapper mapper, ICardStore cardStore)
        {
            _mapper = mapper;
            _cardStore = cardStore;
        }

        public async Task<long> Handle(CreatePlayerCardCommand request, CancellationToken cancellationToken)
        {
            var domain = _mapper.Map<PlayerCard>(request.Dto);

            return await _cardStore.CreatePlayerCardAsync(domain, cancellationToken);
        }

        public async Task<Unit> Handle(CardEffectQuery request, CancellationToken cancellationToken)
        {
            

            return Unit.Value;
        }

        public Task<long> Handle(CreateGameBoardCardCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Unit> Handle(PlayCardCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}