using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Interfaces;

using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;
using UserIdentity.BLL.Application.Exceptions;

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class LobbyCommandHandler : 
        IRequestHandler<CreateLobbyAccountCommand, Unit>,
        IRequestHandler<DeleteLobbyAccountCommand, Unit>,
        IRequestHandler<CreateLobbyCommand, string>,
        IRequestHandler<DeleteLobbyAccountByOwnerCommand, Unit>,
        IRequestHandler<UpdateLobbyInviteFalseCommand, Unit>,
        IRequestHandler<UpdateLobbyInviteTrueCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILobbyStore _lobbyStore;
        private readonly IAccountStore _accountStore;

        public LobbyCommandHandler(IMapper mapper, ILobbyStore lobbyStore, IAccountStore accountStore)
        {
            _mapper = mapper;
            _lobbyStore = lobbyStore;
            _accountStore = accountStore;
        }

        public async Task<Unit> Handle(CreateLobbyAccountCommand request, CancellationToken cancellationToken)
        {
            await _lobbyStore.CreateLobbyAccountAsync(_accountStore.GetActualAccountId(), request.Password, cancellationToken);

            return Unit.Value;
        }
        public async Task<Unit> Handle(DeleteLobbyAccountCommand request, CancellationToken cancellationToken)
        {
            await _lobbyStore.DeleteLobbyAccountAsync(request.LobbyId, _accountStore.GetActualAccountId(), cancellationToken);

            return Unit.Value;
        }

        public async Task<string> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            return await _lobbyStore.CreateLobbyAsync(_accountStore.GetActualAccountId(), cancellationToken);
        }

        public async Task<Unit> Handle(DeleteLobbyAccountByOwnerCommand request, CancellationToken cancellationToken)
        {
            var accountId = await _accountStore.GetAccountIdByName(request.AccountName, cancellationToken);

            await _lobbyStore.DeleteLobbyAccountAsync(request.LobbyId, accountId, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateLobbyInviteFalseCommand request, CancellationToken cancellationToken)
        {
            var friendId = await _accountStore.GetAccountIdByName(request.AccountName, cancellationToken);
            var ownId = _accountStore.GetActualAccountId();

            await _lobbyStore.UpdateLobbyAccountIsInvite(ownId, false, cancellationToken);

            var password = await _lobbyStore.GetPasswordByAccountId(friendId, cancellationToken);
            await _lobbyStore.CreateLobbyAccountAsync(ownId, password, cancellationToken);

            return Unit.Value;
        }

        public Task<Unit> Handle(UpdateLobbyInviteTrueCommand request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();

            await _lobbyStore.UpdateLobbyAccountIsInvite(ownId, true, cancellationToken);

            return Unit.Value;
        }
    }
}
