using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using UserIdentity.BLL.Application.Interfaces.Hubs;

namespace UserIdentity.BLL.Application.Hubs
{
    [Authorize]
    public class FriendHub : Hub<IFriendHub>
    {
        private readonly IHubContext<FriendHub> _hubContext;

        public FriendHub(IHubContext<FriendHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task FriendRequest()
        {
            if (_hubContext.Clients != null)
                await _hubContext.Clients.All.SendAsync("FriendRequest");
        }

        public async Task FriendInvite()
        {
            if (_hubContext.Clients != null)
                await _hubContext.Clients.All.SendAsync("FriendInvite");
        }
    }
}
