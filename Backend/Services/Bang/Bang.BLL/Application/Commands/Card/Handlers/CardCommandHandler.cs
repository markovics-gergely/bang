using Bang.BLL.Application.Interfaces;

using System.Threading.Tasks;
using Bang.BLL.Application.Effects.Cards;
using Bang.BLL.Application.Commands.Commands;
using Bang.DAL.Domain.Joins;

using System.Threading;

using AutoMapper;
using MediatR;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Application.Commands.Handlers
{
    public class CardCommandHandler :
        IRequestHandler<CreateGameBoardCardCommand, long>,
        IRequestHandler<CreatePlayerCardCommand, long>,
        IRequestHandler<PlayCardCommand, Unit>,
        IRequestHandler<DiscardCardCommand, Unit>,
        IRequestHandler<DrawCardCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ICardStore _cardStore;
        private readonly IPlayerStore _playerStore;
        private readonly IGameBoardStore _gameBoardStore;

        public CardCommandHandler(IMapper mapper, ICardStore cardStore, IPlayerStore playerStore, IGameBoardStore gameBoardStore)
        {
            _mapper = mapper;
            _cardStore = cardStore;
            _playerStore = playerStore;
            _gameBoardStore = gameBoardStore;
        }

        public async Task<long> Handle(CreatePlayerCardCommand request, CancellationToken cancellationToken)
        {
            var domain = _mapper.Map<PlayerCard>(request.Dto);

            return await _cardStore.CreatePlayerCardAsync(domain, cancellationToken);
        }

        public Task<long> Handle(CreateGameBoardCardCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Unit> Handle(PlayCardCommand request, CancellationToken cancellationToken)
        {
            if (request.PlayCardDto.TargetPlayerCardId == null && request.PlayCardDto.TargetPlayerId == null)
            {
                await _gameBoardStore.PlayCardAsync(request.PlayCardDto.PlayerCardId, cancellationToken);
            }
            else if (request.PlayCardDto.TargetPlayerCardId != null)
            {
                await _gameBoardStore.PlayCardAsync(request.PlayCardDto.PlayerCardId, (long)request.PlayCardDto.TargetPlayerCardId, false, cancellationToken);
            }
            else
            {
                await _gameBoardStore.PlayCardAsync(request.PlayCardDto.PlayerCardId, (long)request.PlayCardDto.TargetPlayerId, true, cancellationToken);
            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(DiscardCardCommand request, CancellationToken cancellationToken)
        {
            await _playerStore.DiscardCardAsync(request.PlayerCardId, cancellationToken);
            await _gameBoardStore.SetGameBoardPhaseAsync(PhaseEnum.Throwing, cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DrawCardCommand request, CancellationToken cancellationToken)
        {
            await _gameBoardStore.DrawGameBoardCardsFromTopAsync(request.Count, cancellationToken);
            await _gameBoardStore.SetGameBoardPhaseAsync(PhaseEnum.Playing, cancellationToken);
            return Unit.Value;
        }
    }
}
