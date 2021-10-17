using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.BLL.Application.Interfaces.Hubs;
using UserIdentity.DAL.Domain;

namespace UserIdentity.BLL.Application.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        public static HubRoom ChatRoom { get; } = new HubRoom { Name = "ChatRoom" };
        public class HubRoom
        {
            public string Name { get; set; }
            public string CreatorId { get; set; }
            public string Passkey { get; set; }
            public List<Message> Messages { get; } = new List<Message>();
            public List<Account> Accounts { get; } = new List<Account>();
        }

        public async Task EnterLobby()
        {
            var account = new Account { Id = Context.UserIdentifier, UserName = Context.User.Identity.Name };
            ChatRoom.Accounts.Add(account);

            await Clients.Group(ChatRoom.Name)
                .UserEntered(account);

            await Groups.AddToGroupAsync(Context.ConnectionId, ChatRoom.Name);
            await Clients.Caller.SetUsers(ChatRoom.Accounts);
            await Clients.Caller.SetMessages(ChatRoom.Messages);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var account = ChatRoom.Accounts.FirstOrDefault(u => u.Id == Context.UserIdentifier);
            if (account != null)
                ChatRoom.Accounts.Remove(account);

            Clients.Group(ChatRoom.Name).UserLeft(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }
    }
}