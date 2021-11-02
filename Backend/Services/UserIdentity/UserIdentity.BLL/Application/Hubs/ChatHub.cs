using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Application.Interfaces.Hubs;
using UserIdentity.DAL.Domain;

namespace UserIdentity.BLL.Application.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        public const string LobbyRoomName = "ChattRLobby";

        public static Dictionary<string, HubRoom> Rooms { get; set; } = new();
        public static HubRoom Lobby { get; } = new HubRoom
        {
            Name = LobbyRoomName
        };

        public async Task EnterLobby()
        {
            var user = new Account 
            { 
                Id = Context.UserIdentifier, 
                UserName = Context.User.Identity.Name 
            };        
            Lobby.Accounts.Add(user);

            await Clients.Group(LobbyRoomName).UserEntered(user);
            await Groups.AddToGroupAsync(Context.ConnectionId, LobbyRoomName);
            await Clients.Caller.SetUsers(Lobby.Accounts);
            await Clients.Caller.SetMessages(Lobby.Messages);

            var rooms = new List<Room>();
            foreach (var item in Rooms.Values)
            {
                var room = new Room { Name = item.Name };
                rooms.Add(room);
            }
            await Clients.Caller.SetRooms(rooms);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = Lobby.Accounts.FirstOrDefault(u => u.UserName == Context.User.Identity.Name);

            if (user != null)
            {
                Lobby.Accounts.Remove(user);
                Clients.Group(LobbyRoomName).UserLeft(Context.User.Identity.Name);
            }

            var room = Rooms.Values.FirstOrDefault(r => r.Accounts.Any(u => u.UserName == Context.User.Identity.Name));
            if (room != null)
            {
                room.Accounts.Remove(room.Accounts.FirstOrDefault(u => u.UserName == Context.User.Identity.Name));
                if (room.Accounts.Count == 0)
                {
                    Rooms.Remove(room.Name, out HubRoom value);
                    Clients.Group(LobbyRoomName).RoomAbandoned(room.Name);
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToLobby(string message)
        {
            var messageInstance = new Message
            {
                SenderId = Context.UserIdentifier,
                SenderName = Context.User.Identity.Name,
                Text = message,
                PostedDate = DateTimeOffset.Now
            };

            Lobby.Messages.Add(messageInstance);
            await Clients.Group(LobbyRoomName).RecieveMessage(messageInstance);
        }

        public async Task SendMessageToRoom(string message, string room)
        {
            var messageInstance = new Message
            {
                SenderId = Context.UserIdentifier,
                SenderName = Context.User.Identity.Name,
                Text = message,
                PostedDate = DateTimeOffset.Now
            };

            Rooms[room].Messages.Add(messageInstance);
            await Clients.Group(room).RecieveMessage(messageInstance);
        }

        public async Task CreateRoom(string room)
        {
            if (!Rooms.ContainsKey(room))
            {
                Rooms.Add(room, new HubRoom { Name = room, CreatorId = Context.User.Identity.Name });
                await Clients.Group(LobbyRoomName).RoomCreated(new Room { Name = room });
                await Clients.Caller.JoinRoom(new Room { Name = room });
            }
        }

        public async Task EnterRoom(string roomId)
        {
            var user = new Account 
            { 
                Id = Context.UserIdentifier, 
                UserName = Context.User.Identity.Name 
            };
            var room = Rooms[roomId];

            room.Accounts.Add(user);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Caller.SetMessages(room.Messages);
        }
    }

    public class HubRoom
    {
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public string Passkey { get; set; }
        public List<Message> Messages { get; } = new List<Message>();
        public List<Account> Accounts { get; } = new List<Account>();
    }
}