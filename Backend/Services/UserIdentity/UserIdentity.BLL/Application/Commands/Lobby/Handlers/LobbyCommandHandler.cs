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

namespace UserIdentity.BLL.Application.Commands.Handlers
{
    public class LobbyCommandHandler : 
        IRequestHandler<CreateLobbyAccountCommand, Unit>,
        IRequestHandler<DeleteLobbyAccountCommand, Unit>,
        IRequestHandler<CreateLobbyCommand, long>,
        IRequestHandler<DeleteLobbyAccountByOwnerCommand, Unit>,
        IRequestHandler<UpdateLobbyInviteFalseCommand, Unit>,
        IRequestHandler<UpdateLobbyInviteTrueCommand, Unit>,
        IRequestHandler<CreateGameBoardCommand, Unit>
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
            var accountId = await _accountStore.GetAccountIdByName(request.AccountName, cancellationToken);

            await _lobbyStore.DeleteLobbyAccountAsync(request.LobbyId, accountId, cancellationToken);

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

            await _friendStore.UpdateIsInviteAsync(ownId, friendId, true, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(CreateGameBoardCommand request, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(request.Dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"gameboard", data);
            var gameBoardId = Int64.Parse(await response.Content.ReadAsStringAsync());

            var lobby = await _lobbyStore.GetLobbyByIdAsync(request.LobbyId, cancellationToken);
            lobby.GameBoardId = gameBoardId;

            await _lobbyStore.UpdateLobbyAsync(lobby, cancellationToken);

            return Unit.Value;
        }
    }
}
