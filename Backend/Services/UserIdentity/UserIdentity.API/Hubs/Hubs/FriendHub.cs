using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.API.Hubs.Interfaces;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL.Domain;

namespace UserIdentity.API.Hubs.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FriendHub : Hub<IFriendHub>
    {
        private readonly IAccountStore _accountStore;
        private readonly IFriendStore _friendStore;

        public static ConcurrentDictionary<string, string> Connections { get; } = new ConcurrentDictionary<string, string>();

        public FriendHub(IAccountStore accountStore, IFriendStore friendStore)
        {
            _accountStore = accountStore;
            _friendStore = friendStore;
        }

        public async override Task<Task> OnConnectedAsync()
        {
            var actName = await _accountStore.GetActualAccountName();

            Connections.TryAdd(Context.ConnectionId, actName);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Connections.TryRemove(Context.ConnectionId, out _);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task AddFriend(string receiverName)
        {
            var conId = Connections.FirstOrDefault(x => x.Value == receiverName).Key;

            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();

            await Clients.Client(conId).RefreshFriendList();
        }

        public async Task AcceptFriendRequest(string receiverName)
        {
            var conId = Connections.FirstOrDefault(x => x.Value == receiverName).Key;

            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();

            await Clients.Client(conId).RefreshFriendList();
        }

        public async Task InviteFriend(string receiverName)
        {
            var conId = Connections.FirstOrDefault(x => x.Value == receiverName).Key;

            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();

            await Clients.Client(conId).RefreshFriendList();
        }

        public async Task RemoveFriend(string receiverName)
        {
            var conId = Connections.FirstOrDefault(x => x.Value == receiverName).Key;

            var actId = _accountStore.GetActualAccountId();
            var actName = await _accountStore.GetActualAccountName();

            await Clients.Client(conId).RefreshFriendList();
        }
    }
}
