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

        public static ConcurrentDictionary<string, HubRoom> Rooms { get; set; } = new ConcurrentDictionary<string, HubRoom>();

        public ChatHub(IAccountStore accountStore, ILobbyStore lobbyStore)
        {
            _accountStore = accountStore;
            _lobbyStore = lobbyStore;
        }

        public async override Task<Task> OnDisconnectedAsync(Exception exception)
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();

            var room = Rooms.Values.FirstOrDefault(r => r.Accounts.Any(u => u.UserName == actName));
            if (room != null)
            {
                room.Accounts.Remove(room.Accounts.FirstOrDefault(u => u.UserName == actName));
                if (room.Accounts.Count == 0)
                {
                    Rooms.Remove(room.Name, out HubRoom value);
                }
            }
            return base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessageToRoom(string message)
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var roomPassword = (await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken())).Password;

            var messageInstance = new Message
            {
                UserName = actName,
                Text = message,
            };

            Rooms[roomPassword].Messages.Add(messageInstance);
            await Clients.Group(roomPassword).SetMessage(messageInstance);
        }

        public async Task EnterRoom()
        {
            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();
            var roomPassword = (await _lobbyStore.GetActualLobbyAsync(actId, new CancellationToken())).Password;

            if (!Rooms.ContainsKey(roomPassword))
            {
                Rooms.TryAdd(roomPassword, new HubRoom { Name = roomPassword, CreatorId = actId });
            }

            var user = new Account { Id = actId, UserName = actName };
            var room = Rooms[roomPassword];

            room.Accounts.Add(user);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomPassword);
            await Clients.Caller.SetMessages(room.Messages);
        }
    }

    public class HubRoom
    {
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public List<Message> Messages { get; } = new List<Message>();
        public List<Account> Accounts { get; } = new List<Account>();
    }
}