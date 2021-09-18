using Bang.BLL.Application.Interfaces;
using Bang.BLL.Application.Commands.Commands;
using Bang.DAL.Domain;
using Bang.DAL.Domain.Catalog.Cards;
using Bang.BLL.Application.Commands.DataTransferObjects;
using Bang.DAL.Domain.Joins.GameBoardCards;
using Bang.DAL.Domain.Constants;
using Bang.BLL.Application.Exceptions;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;
using Bang.DAL.Domain.Joins;
using Bang.BLL.Application.Commands.ViewModels;

using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

using AutoMapper;
using MediatR;
using Bang.DAL.Domain.Joins.PlayerCards;

namespace Bang.BLL.Application.Commands.Handlers
{
    public class PlayerCommandHandler :
        IRequestHandler<DecrementPlayerHealthCommand>
    {
        private readonly IMapper _mapper;
        private readonly IGameBoardStore _gameBoardStore;
        private readonly ICardStore _cardStore;
        private readonly IPlayerStore _playerStore;
        private readonly IRoleStore _roleStore;
        private readonly ICharacterStore _characterStore;

        public PlayerCommandHandler(IMapper mapper, IGameBoardStore gameBoardStore, 
            ICardStore cardStore, IPlayerStore playerStore, IRoleStore roleStore, ICharacterStore characterStore)
        {
            _mapper = mapper;
            _gameBoardStore = gameBoardStore;
            _cardStore = cardStore;
            _playerStore = playerStore;
            _roleStore = roleStore;
            _characterStore = characterStore;
        }

        public async Task<Unit> Handle(DecrementPlayerHealthCommand request, CancellationToken cancellationToken)
        {
            long newHP = await _playerStore.DecrementPlayerHealthAsync(request.PlayerId, cancellationToken);
            //TODO game end
            return Unit.Value;
        }
    }
}
