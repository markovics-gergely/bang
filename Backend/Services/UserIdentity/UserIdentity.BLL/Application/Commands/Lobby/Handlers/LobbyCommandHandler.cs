using UserIdentity.BLL.Application.Commands.Commands;
using UserIdentity.BLL.Application.Interfaces;

using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System.Text;
using UserIdentity.DAL.Domain;
using System;
using UserIdentity.BLL.Application.Exceptions;
using System.Linq;

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class LobbyCommandHandler : 
        IRequestHandler<CreateLobbyAccountCommand, Unit>,
        IRequestHandler<DeleteLobbyAccountCommand, Unit>,
        IRequestHandler<CreateLobbyCommand, long>,
        IRequestHandler<DeleteLobbyAccountByOwnerCommand, Unit>,
        IRequestHandler<UpdateLobbyInviteFalseCommand, Unit>,
        IRequestHandler<UpdateLobbyInviteTrueCommand, Unit>,
        IRequestHandler<CreateGameBoardCommand, Unit>,
        IRequestHandler<UpdateLobbyGameBoardIdCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILobbyStore _lobbyStore;
        private readonly IAccountStore _accountStore;
        private readonly IFriendStore _friendStore;
        private readonly HttpClient _httpClient;

        public LobbyCommandHandler(IMapper mapper, ILobbyStore lobbyStore, IAccountStore accountStore, IFriendStore friendStore, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _lobbyStore = lobbyStore;
            _accountStore = accountStore;
            _friendStore = friendStore;
            _httpClient = httpClientFactory.CreateClient("bang");
        }

        public async Task<Unit> Handle(CreateLobbyAccountCommand request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();

            await _lobbyStore.CreateLobbyAccountAsync(ownId, request.Password, cancellationToken);

            return Unit.Value;
        }
        public async Task<Unit> Handle(DeleteLobbyAccountCommand request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();

            await _lobbyStore.DeleteLobbyAccountAsync(request.LobbyId, ownId, cancellationToken);

            return Unit.Value;
        }

        public async Task<long> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();

            return await _lobbyStore.CreateLobbyAsync(ownId, cancellationToken);
        }

        public async Task<Unit> Handle(DeleteLobbyAccountByOwnerCommand request, CancellationToken cancellationToken)
        {
            var ownId = await _accountStore.GetAccountIdByName(request.AccountName, cancellationToken);

            await _lobbyStore.DeleteLobbyAccountAsync(request.LobbyId, ownId, cancellationToken);

            await _friendStore.UpdateIsInviteAsync(ownId, false, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateLobbyInviteFalseCommand request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();
            var friendId = await _accountStore.GetAccountIdByName(request.AccountName, cancellationToken);

            await _friendStore.UpdateIsInviteAsync(friendId, ownId, false, cancellationToken);

            var password = await _lobbyStore.GetPasswordByAccountIdAsync(friendId, cancellationToken);
            await _lobbyStore.CreateLobbyAccountAsync(ownId, password, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateLobbyInviteTrueCommand request, CancellationToken cancellationToken)
        {
            var ownId = _accountStore.GetActualAccountId();
            var friendId = await _accountStore.GetAccountIdByName(request.AccountName, cancellationToken);

            if(await _lobbyStore.GetActualLobbyAsync(friendId, cancellationToken) != null)
            {
                throw new InvalidActionException("Friend is already in a lobby!");
            }

            await _friendStore.UpdateIsInviteAsync(ownId, friendId, true, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(CreateGameBoardCommand request, CancellationToken cancellationToken)
        {
            var accounts = (await _lobbyStore.GetLobbyAccountsAsync(request.LobbyId, cancellationToken)).ToList();
            if(accounts.Count < 4 || accounts.Count > 7)
            {
                throw new InvalidActionException("Minimum 4, maximum 7 people!");
            }

            await _friendStore.UpdateIsInviteForAccountsAsync(accounts, false, cancellationToken);

            var lobby = await _lobbyStore.GetLobbyByIdAsync(request.LobbyId, cancellationToken);
            lobby.GameBoardId = request.GameBoardId;

            await _lobbyStore.UpdateLobbyAsync(lobby, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateLobbyGameBoardIdCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbyStore.GetLobbyByOwnerIdAsync(request.OwnerId, cancellationToken);

            lobby.GameBoardId = 0;

            await _lobbyStore.UpdateLobbyAsync(lobby, cancellationToken);

            return Unit.Value;
        }
    }
}
