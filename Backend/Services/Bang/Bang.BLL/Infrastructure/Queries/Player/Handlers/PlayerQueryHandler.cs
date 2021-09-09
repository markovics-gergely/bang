using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class PlayerQueryHandler :
        IRequestHandler<GetPlayerQuery, PlayerViewModel>,
        IRequestHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>,
        IRequestHandler<GetPlayersByGameBoardQuery, IEnumerable<PlayerViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IPlayerStore _playerStore;

        public PlayerQueryHandler(IMapper mapper, IPlayerStore playerStore)
        {
            _mapper = mapper;
            _playerStore = playerStore;
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
    }
}
