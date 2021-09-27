using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Bang.DAL.Domain;
using UserIdentity.BLL.Application.Interfaces;
using Bang.BLL.Application.Exceptions;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class GameBoardQueryHandler :
        IRequestHandler<GetGameBoardQuery, GameBoardViewModel>,
        IRequestHandler<GetGameBoardsQuery, IEnumerable<GameBoardViewModel>>,
        IRequestHandler<GetGameBoardCardsOnTopQuery, IEnumerable<FrenchCardViewModel>>,
        IRequestHandler<GetLastDiscardedGameBoardCardQuery, FrenchCardViewModel>,
        IRequestHandler<GetGameBoardByUserQuery, GameBoardByUserViewModel>
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

        public async Task<GameBoardByUserViewModel> Handle(GetGameBoardByUserQuery request, CancellationToken cancellationToken)
        {
            var domain = await _gameBoardStore.GetGameBoardByUserAsync(request.UserId, cancellationToken);
            List<Player> players = new List<Player>(domain.Players);
            Player player = players.Find(p => p.UserId == request.UserId) ?? throw new EntityNotFoundException("Player not found");
            var ownPlayer = _mapper.Map<PlayerViewModel>(player);

            int ownIndex = players.IndexOf(player);
            int count = players.Count;
            List<Player> otherplayers;
            if(ownIndex == 0)
            {
                otherplayers = players.GetRange(1, count - 1);
            }
            else if(ownIndex == count - 1)
            {
                otherplayers = players.GetRange(0, count - 1);
            }
            else
            {
                otherplayers = players.GetRange(ownIndex + 1, count - ownIndex - 1);
                otherplayers.AddRange(players.GetRange(0, ownIndex));
            }
            var otherplayersViewModel = _mapper.Map<ICollection<PlayerByUserViewModel>>(otherplayers);
            return _mapper.Map<GameBoardByUserViewModel>(domain,
                opt => opt.AfterMap((src, dest) => {
                        dest.OtherPlayers = otherplayersViewModel;
                        dest.OwnPlayer = ownPlayer;
                        })
                );
        }
    }
}
