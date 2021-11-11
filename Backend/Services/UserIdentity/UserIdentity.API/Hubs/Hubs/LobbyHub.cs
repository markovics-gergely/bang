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

        public async override Task<Task> OnDisconnectedAsync(Exception exception)
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();

            var lobby = Lobbies.Values.FirstOrDefault(r => r.Accounts.Any(u => u.UserName == actName));
            if (lobby != null)
            {
                lobby.Accounts.Remove(lobby.Accounts.FirstOrDefault(u => u.UserName == actName));
                if (lobby.Accounts.Count == 0)
                {
                    Lobbies.Remove(lobby.Name, out HubLobby value);
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task LeaveLobby()
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var dbLobby = await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken());

            var lobby = Lobbies.Values.FirstOrDefault(r => r.Accounts.Any(u => u.UserName == actName));
            if (lobby != null)
            {
                lobby.Accounts.Remove(lobby.Accounts.FirstOrDefault(u => u.UserName == actName));
                if (lobby.Accounts.Count == 0)
                {
                    Lobbies.Remove(lobby.Name, out HubLobby value);
                }     
            }

            await Clients.Group(dbLobby.Password).RefreshLobbyUsers(dbLobby.Id);
        }

        public async Task SendMessageToRoom(string message)
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var lobby = await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken());

            var messageInstance = new Message
            {
                UserName = actName,
                Text = message,
            };

            Lobbies[lobby.Password].Messages.Add(messageInstance);
            await Clients.Group(lobby.Password).SetMessage(messageInstance);
        }

        public async Task EnterRoom()
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var lobby = await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken());

            if (!Lobbies.ContainsKey(lobby.Password))
            {
                Lobbies.TryAdd(lobby.Password, new HubLobby { Name = lobby.Password, OwnerId = lobby.OwnerId });
            }

            var user = new Account { Id = actId, UserName = actName };
            var room = Lobbies[lobby.Password];

            room.Accounts.Add(user);

            await Groups.AddToGroupAsync(Context.ConnectionId, lobby.Password);
            await Clients.Caller.SetMessages(room.Messages);
            await Clients.Group(lobby.Password).RefreshLobbyUsers(lobby.Id);
        }
    }

    public class HubLobby
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public List<Message> Messages { get; } = new List<Message>();
        public List<Account> Accounts { get; } = new List<Account>();
    }
}