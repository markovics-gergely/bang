using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Bang.BLL.Infrastructure.Queries.GameBoard.ViewModels;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class PlayerQueryHandler :
        IRequestHandler<GetPlayerQuery, PlayerViewModel>,
        IRequestHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetPlayersByGameBoardQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetTargetablePlayersQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetPermissionsQuery, PermissionViewModel>
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
            var userId = await _accountStore.GetActualAccountIdAsync(cancellationToken);
            var board = await _gameBoardStore.GetGameBoardByUserAsync(userId, cancellationToken);
            if(userId != board.ActualPlayer.UserId && userId != board.TargetedPlayer.UserId)
            {
                permission.CanDoAnything = false;
                return permission;
            }
            if (userId == board.TargetedPlayer.UserId)
            {
                var targeted = board.TargetedPlayer;
                var actual = board.ActualPlayer;
                permission.SetByTargetReason(board.TargetReason);
            }


            return permission;
        }
    }
}
