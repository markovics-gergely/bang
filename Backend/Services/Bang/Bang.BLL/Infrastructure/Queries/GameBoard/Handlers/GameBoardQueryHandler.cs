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
    public class GameBoardQueryHandler :
        IRequestHandler<GetGameBoardQuery, GameBoardViewModel>,
        IRequestHandler<GetGameBoardsQuery, IEnumerable<GameBoardViewModel>>,
        IRequestHandler<GetGameBoardCardsOnTopQuery, IEnumerable<FrenchCardViewModel>>,
        IRequestHandler<GetLastDiscardedGameBoardCardQuery, FrenchCardViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IGameBoardStore _gameBoardStore;

        public GameBoardQueryHandler(IMapper mapper, IGameBoardStore gameBoardStore)
        {
            _mapper = mapper;
            _gameBoardStore = gameBoardStore;
        }

        public async Task<GameBoardViewModel> Handle(GetGameBoardQuery request, CancellationToken cancellationToken)
        {
            var domain = await _gameBoardStore.GetGameBoardAsync(request.Id, cancellationToken);

            return _mapper.Map<GameBoardViewModel>(domain);
        }

        public async Task<IEnumerable<GameBoardViewModel>> Handle(GetGameBoardsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _gameBoardStore.GetGameBoardsAsync(cancellationToken);

            return _mapper.Map<IEnumerable<GameBoardViewModel>>(domain);
        }

        public async Task<IEnumerable<FrenchCardViewModel>> Handle(GetGameBoardCardsOnTopQuery request, CancellationToken cancellationToken)
        {
            var domain = await _gameBoardStore.GetDrawableGameBoardCardsOnTopAsync(request.Id, request.Count, cancellationToken);

            return _mapper.Map<IEnumerable<FrenchCardViewModel>>(domain);
        }

        public async Task<FrenchCardViewModel> Handle(GetLastDiscardedGameBoardCardQuery request, CancellationToken cancellationToken)
        {
            var domain = await _gameBoardStore.GetLastDiscardedGameBoardCardAsync(request.Id, cancellationToken);

            return _mapper.Map<FrenchCardViewModel>(domain);
        }
    }
}
