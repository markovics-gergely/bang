using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserIdentity.API.Hubs.Interfaces;
using UserIdentity.BLL.Application.Interfaces;
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

        public async Task AddFriend(string receiverName)
        {

            //SetFriendRequest a receiverName-nek
        }

        public async Task AcceptFriendRequest(string receiverName)
        {

            //SetFriend a receiverName-nek
        }

        public async Task InviteFriend(string receiverName)
        {

            //SetFriendInvite a receiverName-nek
        }
    }
}
