using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.Queries;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using System.IdentityModel.Tokens.Jwt;

namespace UserIdentity.BLL.Infrastructure.Queries.Handlers
{
    public class AccountQueryHandler :
        IRequestHandler<GetActualAccountIdQuery, string>,
        IRequestHandler<GetActualAccountStatusQuery, StatusViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IAccountStore _accountStore;
        private readonly ILobbyStore _lobbyStore;

        public AccountQueryHandler(IMapper mapper, IAccountStore accountStore, ILobbyStore lobbyStore)
        {
            _mapper = mapper;
            _accountStore = accountStore;
            _lobbyStore = lobbyStore;
        }

        public async Task<string> Handle(GetActualAccountIdQuery request, CancellationToken cancellationToken)
        {
            return _accountStore.GetActualAccountId();
        }

        public async Task<StatusViewModel> Handle(GetActualAccountStatusQuery request, CancellationToken cancellationToken)
        {
            var status = new StatusViewModel();

            var accountId = _accountStore.GetActualAccountId();
            var actualLobby = await _lobbyStore.GetActualLobbyAsync(accountId, cancellationToken);

            if(actualLobby == null)
            {
                status.LobbyId = null;
                status.GameBoardId = null;
            }
            else if (actualLobby.GameBoardId == 0)
            {
                status.LobbyId = actualLobby.Id;
                status.GameBoardId = null;
            }
            else
            {
                status.LobbyId = actualLobby.Id;
                status.GameBoardId = actualLobby.GameBoardId;
            }

            return status;
        }
    }
}
