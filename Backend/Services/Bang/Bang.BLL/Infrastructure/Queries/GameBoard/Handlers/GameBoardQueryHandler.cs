using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.BLL.Infrastructure.Queries.Queries;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Bang.DAL.Domain;
using Bang.BLL.Application.Exceptions;

namespace Bang.BLL.Infrastructure.Queries.Handlers
{
    public class GameBoardQueryHandler :
        IRequestHandler<GetGameBoardQuery, GameBoardViewModel>,
        IRequestHandler<GetGameBoardsQuery, IEnumerable<GameBoardViewModel>>,
        IRequestHandler<GetGameBoardCardsOnTopQuery, IEnumerable<FrenchCardViewModel>>,
        IRequestHandler<GetLastDiscardedGameBoardCardQuery, FrenchCardViewModel>,
        IRequestHandler<GetGameBoardByUserQuery, GameBoardByUserViewModel>,
        IRequestHandler<GetGameBoardByUserIdQuery, GameBoardByUserViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IGameBoardStore _gameBoardStore;
        private readonly IPlayerStore _playerStore;
        private readonly IAccountStore _accountStore;

        public GameBoardQueryHandler(IMapper mapper, IGameBoardStore gameBoardStore, IAccountStore accountStore, IPlayerStore playerStore)
        {
            _mapper = mapper;
            _gameBoardStore = gameBoardStore;
            _playerStore = playerStore;
            _accountStore = accountStore;
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
            var domain = await _gameBoardStore.GetDrawableGameBoardCardsOnTopAsync(request.Count, cancellationToken);

            return _mapper.Map<IEnumerable<FrenchCardViewModel>>(domain);
        }

        public async Task<FrenchCardViewModel> Handle(GetLastDiscardedGameBoardCardQuery request, CancellationToken cancellationToken)
        {
            var domain = await _gameBoardStore.GetLastDiscardedGameBoardCardAsync(cancellationToken);

            return _mapper.Map<FrenchCardViewModel>(domain);
        }

        public async Task<GameBoardByUserViewModel> Handle(GetGameBoardByUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _accountStore.GetActualAccountId();
            var domain = await _gameBoardStore.GetGameBoardByUserAsync(userId, cancellationToken);
            List<Player> players = new List<Player>(domain.Players);
            Player player = players.Find(p => p.UserId == userId) ?? throw new EntityNotFoundException("Player not found");
            var targetables = (await _playerStore.GetTargetablePlayersAsync(player.Id, cancellationToken)).Select(p => p.Id);
            var ownPlayer = _mapper.Map<PlayerViewModel>(player,
                opt => opt.AfterMap((src, dest) => {
                    dest.TargetablePlayers = new List<long>(targetables);
                }));

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

        public async Task<GameBoardByUserViewModel> Handle(GetGameBoardByUserIdQuery request, CancellationToken cancellationToken)
        {
            var domain = await _gameBoardStore.GetGameBoardByUserIdAsync(request.UserId, cancellationToken);
            List<Player> players = new List<Player>(domain.Players);
            Player player = players.Find(p => p.UserId == request.UserId) ?? throw new EntityNotFoundException("Player not found");
            var targetables = (await _playerStore.GetTargetablePlayersAsync(player.Id, cancellationToken)).Select(p => p.Id);
            var ownPlayer = _mapper.Map<PlayerViewModel>(player,
                opt => opt.AfterMap((src, dest) => {
                    dest.TargetablePlayers = new List<long>(targetables);
                }));

            int ownIndex = players.IndexOf(player);
            int count = players.Count;
            List<Player> otherplayers;
            if (ownIndex == 0)
            {
                otherplayers = players.GetRange(1, count - 1);
            }
            else if (ownIndex == count - 1)
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
                }));
        }
    }
}
