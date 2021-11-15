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
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IAccountStore _accountStore;
        private readonly ILobbyStore _lobbyStore;

        public static ConcurrentDictionary<string, HubChatLobby> Lobbies { get; set; } = new ConcurrentDictionary<string, HubChatLobby>();

        public ChatHub(IAccountStore accountStore, ILobbyStore lobbyStore)
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
                    Lobbies.TryAdd(lobby.Password, new HubChatLobby { Name = lobby.Password });
                }

                var user = new Account { Id = actId, UserName = actName };
                var room = Lobbies[lobby.Password];

                room.Accounts.Add(user);

                await Groups.AddToGroupAsync(Context.ConnectionId, lobby.Password);
                await Clients.Caller.SetMessages(room.Messages);
            }
        }

        public async Task LeaveLobby()
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var dbLobby = await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken());

            var lobby = Lobbies.Values.FirstOrDefault(r => r.Accounts.Any(u => u.UserName == actName));
            if (lobby != null && dbLobby != null)
            {
                lobby.Accounts.Remove(lobby.Accounts.FirstOrDefault(u => u.UserName == actName));
                if (lobby.Accounts.Count == 0)
                {
                    Lobbies.Remove(lobby.Name, out HubChatLobby value);
                }
            }
        }       

        public async Task SendMessageToLobby(string message)
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var lobby = await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken());

            if (lobby != null)
            {
                var messageInstance = new Message
                {
                    UserName = actName,
                    Text = message,
                };

                Lobbies[lobby.Password].Messages.Add(messageInstance);
                await Clients.Group(lobby.Password).SetMessage(messageInstance);
            }
        }
    }

    public class HubChatLobby
    {
        public string Name { get; set; }
        public List<Message> Messages { get; } = new List<Message>();
        public List<Account> Accounts { get; } = new List<Account>();
    }
}