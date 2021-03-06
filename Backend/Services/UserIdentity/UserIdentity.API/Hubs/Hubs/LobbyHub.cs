using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Application.Interfaces.Hubs;
using UserIdentity.DAL.Domain;

namespace UserIdentity.API.Hubs.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LobbyHub : Hub<ILobbyHub>
    {
        private readonly IAccountStore _accountStore;
        private readonly ILobbyStore _lobbyStore;

        public static ConcurrentDictionary<string, HubLobby> Lobbies { get; set; } = new ConcurrentDictionary<string, HubLobby>();

        public LobbyHub(IAccountStore accountStore, ILobbyStore lobbyStore)
        {
            _accountStore = accountStore;
            _lobbyStore = lobbyStore;
        }

        public async Task EnterLobby()
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var lobby = await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken());

            if (lobby != null)
            {
                if (!Lobbies.ContainsKey(lobby.Password))
                {
                    Lobbies.TryAdd(lobby.Password, new HubLobby { Name = lobby.Password });
                }

                var user = new Account { Id = actId, UserName = actName };
                var room = Lobbies[lobby.Password];

                room.Accounts.Add(user);

                await Groups.AddToGroupAsync(Context.ConnectionId, lobby.Password);
                await Clients.Group(lobby.Password).RefreshLobbyUsers(lobby.Id);
            }
        }

        public async Task LeaveLobby(string password, long lobbyId)
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();

            await Clients.Group(password).RefreshLobbyUsers(lobbyId);

            var lobby = Lobbies.Values.FirstOrDefault(r => r.Accounts.Any(u => u.UserName == actName));
            if (lobby != null)
            {
                lobby.Accounts.Remove(lobby.Accounts.FirstOrDefault(u => u.UserName == actName));
                if (lobby.Accounts.Count == 0)
                {
                    Lobbies.Remove(lobby.Name, out HubLobby value);
                }        
            }
        }       

        public async Task NavigateToGameboard()
        {
            var actId = _accountStore.GetActualAccountId();
            var lobby = await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken());

            var clients = Clients.Group(lobby.Password);
            await clients.NavigateToGameboard();
        }
    }

    public class HubLobby
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; } = new List<Account>();
    }
}