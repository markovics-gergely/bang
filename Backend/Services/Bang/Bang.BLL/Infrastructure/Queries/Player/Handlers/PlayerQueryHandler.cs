using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

using AutoMapper;
using MediatR;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class PlayerQueryHandler :
        IRequestHandler<GetPlayerQuery, PlayerViewModel>,
        IRequestHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetPlayersByGameBoardQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetTargetablePlayersQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetTargetablePlayersByRangeQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetPermissionsQuery, PermissionViewModel>,
        IRequestHandler<GetPermissionsByUserQuery, PermissionViewModel>,
        IRequestHandler<GetPermissionsByGameBoardQuery, PermissionViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IPlayerStore _playerStore;
        private readonly IGameBoardStore _gameBoardStore;
        private readonly IAccountStore _accountStore;

        public PlayerQueryHandler(IMapper mapper, IPlayerStore playerStore, IGameBoardStore gameBoardStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _playerStore = playerStore;
            _gameBoardStore = gameBoardStore;
            _accountStore = accountStore;
        }

        public async Task<PlayerViewModel> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var domain = await _playerStore.GetPlayerAsync(request.Id, cancellationToken);

            return _mapper.Map<PlayerViewModel>(domain);
        }

        public async Task<IEnumerable<PlayerViewModel>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var domain = await _playerStore.GetPlayersAsync(cancellationToken);

            return _mapper.Map<IEnumerable<PlayerViewModel>>(domain);
        }

        public async Task<IEnumerable<PlayerViewModel>> Handle(GetPlayersByGameBoardQuery request, CancellationToken cancellationToken)
        {
            var domain = await _playerStore.GetPlayersByGameBoardAsync(request.gameBoardId, cancellationToken);

            return _mapper.Map<IEnumerable<PlayerViewModel>>(domain);
        }

        public async Task<IEnumerable<PlayerViewModel>> Handle(GetTargetablePlayersQuery request, CancellationToken cancellationToken)
        {
            var domain = await _playerStore.GetTargetablePlayersAsync(request.Id, cancellationToken);

            return _mapper.Map<IEnumerable<PlayerViewModel>>(domain);
        }

        public async Task<PermissionViewModel> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permission = new PermissionViewModel();
            var userId = _accountStore.GetActualAccountId();
            var board = await _gameBoardStore.GetGameBoardByUserSimplifiedAsync(userId, cancellationToken);
            var actual = board.Players.FirstOrDefault(p => p.Id == board.ActualPlayerId);
            var targeted = board.Players.FirstOrDefault(p => p.Id == board.TargetedPlayerId);

            if (board == null || (userId != actual.UserId && userId != targeted?.UserId))
            {
                permission.CanDoAnything = false;
                return permission;
            }
            else if (userId == targeted?.UserId)
            {
                permission.SetByTargetReason(board.TargetReason, targeted, actual);
            }
            else if (userId == actual.UserId)
            {
                if (board.TargetedPlayerId != null)
                {
                    permission.CanDoAnything = false;
                    return permission;
                }
                switch (board.TurnPhase)
                {
                    case PhaseEnum.Discarding:
                        permission.CanDiscardFromDrawCard = true;
                        break;
                    case PhaseEnum.Drawing:
                        permission.CanDrawCard = true;
                        if (actual.CharacterType == CharacterType.JesseJones)
                        {
                            permission.CanDrawFromOthersHands = true;
                        }
                        break;
                    case PhaseEnum.Playing:
                        permission.CanEndTurn = true;
                        permission.CanPlayCard = true;
                        permission.CanPlayMissedCard = true;
                        permission.CanDiscardCard = true;
                        if (actual.PlayedCards.Contains(CardType.Bang) && 
                            (actual.CharacterType == CharacterType.WillyTheKid || 
                            actual.TablePlayerCards.Select(t => t.Card.CardType).Contains(CardType.Volcanic)))
                        {
                            permission.CanPlayBangCard = true;
                        }
                        else if (!actual.PlayedCards.Contains(CardType.Bang))
                        {
                            permission.CanPlayBangCard = true;
                        }
                        if (board.Players.Count(p => p.ActualHP > 0) > 2)
                        {
                            permission.CanPlayBeerCard = true;
                        }
                        break;
                    case PhaseEnum.Throwing:
                        permission.CanEndTurn = true;
                        permission.CanDiscardCard = true;
                        break;
                }
            }
            return permission;
        }

        public async Task<PermissionViewModel> Handle(GetPermissionsByUserQuery request, CancellationToken cancellationToken)
        {
            var permission = new PermissionViewModel();
            var userId = request.UserId;
            var board = await _gameBoardStore.GetGameBoardByUserSimplifiedAsync(userId, cancellationToken);
            var actual = board.Players.FirstOrDefault(p => p.Id == board.ActualPlayerId);
            var targeted = board.Players.FirstOrDefault(p => p.Id == board.TargetedPlayerId);

            if (board == null || (userId != actual.UserId && userId != targeted?.UserId))
            {
                permission.CanDoAnything = false;
                return permission;
            }
            else if (userId == targeted?.UserId)
            {
                permission.SetByTargetReason(board.TargetReason, targeted, actual);
            }
            else if (userId == actual.UserId)
            {
                if (board.TargetedPlayerId != null)
                {
                    permission.CanDoAnything = false;
                    return permission;
                }
                switch (board.TurnPhase)
                {
                    case PhaseEnum.Discarding:
                        permission.CanDiscardFromDrawCard = true;
                        break;
                    case PhaseEnum.Drawing:
                        permission.CanDrawCard = true;
                        if (actual.CharacterType == CharacterType.JesseJones)
                        {
                            permission.CanDrawFromOthersHands = true;
                        }
                        break;
                    case PhaseEnum.Playing:
                        permission.CanEndTurn = true;
                        permission.CanPlayCard = true;
                        permission.CanPlayMissedCard = true;
                        permission.CanDiscardCard = true;
                        if (actual.PlayedCards.Contains(CardType.Bang) &&
                            (actual.CharacterType == CharacterType.WillyTheKid ||
                            actual.TablePlayerCards.Select(t => t.Card.CardType).Contains(CardType.Volcanic)))
                        {
                            permission.CanPlayBangCard = true;
                        }
                        else if (!actual.PlayedCards.Contains(CardType.Bang))
                        {
                            permission.CanPlayBangCard = true;
                        }
                        if (board.Players.Count(p => p.ActualHP > 0) > 2)
                        {
                            permission.CanPlayBeerCard = true;
                        }
                        break;
                    case PhaseEnum.Throwing:
                        permission.CanEndTurn = true;
                        permission.CanDiscardCard = true;
                        break;
                }
            }
            return permission;
        }

        public async Task<IEnumerable<PlayerViewModel>> Handle(GetTargetablePlayersByRangeQuery request, CancellationToken cancellationToken)
        {
            var domain = await _playerStore.GetTargetablePlayersByRangeAsync(request.Id, request.Range, cancellationToken);

            return _mapper.Map<IEnumerable<PlayerViewModel>>(domain);
        }

        public async Task<PermissionViewModel> Handle(GetPermissionsByGameBoardQuery request, CancellationToken cancellationToken)
        {
            var permission = new PermissionViewModel();
            var userId = _accountStore.GetActualAccountId();
            var board = request.GameBoard;
            var actual = board.Players.FirstOrDefault(p => p.Id == board.ActualPlayerId);
            var targeted = board.Players.FirstOrDefault(p => p.Id == board.TargetedPlayerId);

            if (board == null || (userId != actual.UserId && userId != targeted?.UserId))
            {
                permission.CanDoAnything = false;
                return permission;
            }
            else if (userId == targeted?.UserId)
            {
                permission.SetByTargetReason(board.TargetReason, targeted, actual);
            }
            else if (userId == actual.UserId)
            {
                if (board.TargetedPlayerId != null)
                {
                    permission.CanDoAnything = false;
                    return permission;
                }
                switch (board.TurnPhase)
                {
                    case PhaseEnum.Discarding:
                        permission.CanDiscardFromDrawCard = true;
                        break;
                    case PhaseEnum.Drawing:
                        permission.CanDrawCard = true;
                        if (actual.CharacterType == CharacterType.JesseJones)
                        {
                            permission.CanDrawFromOthersHands = true;
                        }
                        break;
                    case PhaseEnum.Playing:
                        permission.CanEndTurn = true;
                        permission.CanPlayCard = true;
                        permission.CanPlayMissedCard = true;
                        permission.CanDiscardCard = true;
                        if (actual.PlayedCards.Contains(CardType.Bang) &&
                            (actual.CharacterType == CharacterType.WillyTheKid ||
                            actual.TablePlayerCards.Select(t => t.Card.CardType).Contains(CardType.Volcanic)))
                        {
                            permission.CanPlayBangCard = true;
                        }
                        else if (!actual.PlayedCards.Contains(CardType.Bang))
                        {
                            permission.CanPlayBangCard = true;
                        }
                        if (board.Players.Count(p => p.ActualHP > 0) > 2)
                        {
                            permission.CanPlayBeerCard = true;
                        }
                        break;
                    case PhaseEnum.Throwing:
                        permission.CanEndTurn = true;
                        permission.CanDiscardCard = true;
                        break;
                }
            }
            return permission;
        }
    }
}
