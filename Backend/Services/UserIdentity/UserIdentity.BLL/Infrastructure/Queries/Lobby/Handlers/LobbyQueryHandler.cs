using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.Queries;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using UserIdentity.DAL.Domain;

namespace UserIdentity.BLL.Infrastructure.Queries.Handlers
{
    public class LobbyQueryHandler :
        IRequestHandler<GetLobbyAccountsQuery, IEnumerable<LobbyAccountViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILobbyStore _lobbyStore;

        public LobbyQueryHandler(IMapper mapper, ILobbyStore lobbyStore)
        {
            _mapper = mapper;
            _lobbyStore = lobbyStore;
        }

        public async Task<IEnumerable<LobbyAccountViewModel>> Handle(GetLobbyAccountsQuery request, CancellationToken cancellationToken)
        {
            var domain = await _lobbyStore.GetLobbyAccountsAsync(request.LobbyId, cancellationToken);

            return _mapper.Map<IEnumerable<LobbyAccountViewModel>>(domain);
        }
    }
}
