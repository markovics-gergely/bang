using Bang.BLL.Application.Interfaces;
using Bang.BLL.Application.Commands.Commands;
using Bang.DAL.Domain.Constants.Enums;

using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;
using Bang.BLL.Application.Effects.Characters;
using System.Collections.Generic;
using Bang.BLL.Application.Exceptions;

namespace Bang.BLL.Application.Commands.Handlers
{
    public class PlayerCommandHandler :
        IRequestHandler<DecrementPlayerHealthCommand, Unit>,
        IRequestHandler<GainHealthForCardsCommand, Unit>,
        IRequestHandler<UseCharacterCommand, Unit>
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
            var selectedPlayer = await _playerStore.GetOwnPlayerAsync(cancellationToken);
            long newHP = await _playerStore.DecrementPlayerHealthAsync(cancellationToken);
            await _gameBoardStore.SetGameBoardNextTargetedAsync(selectedPlayer.Id, selectedPlayer.GameBoardId, cancellationToken);

            if (selectedPlayer.CharacterType == CharacterType.BartCassidy)
            {
                await _gameBoardStore.DrawGameBoardCardsFromTopAsync(1, selectedPlayer.Id, cancellationToken);
            }
            if (newHP == 0)
            {
                bool isOver = await _gameBoardStore.CalculatePlayerPlacementAsync(selectedPlayer.Id, cancellationToken);
                if (isOver)
                {
                    await _gameBoardStore.SetGameBoardEndAsync(cancellationToken);
                }
            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(GainHealthForCardsCommand request, CancellationToken cancellationToken)
        {
            await _playerStore.GainHealthForCardsAsync(request.Cards, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UseCharacterCommand request, CancellationToken cancellationToken)
        {
            await _gameBoardStore.UseCharacterAsync(request.CharacterDto, cancellationToken);

            return Unit.Value;
        }
    }
}
