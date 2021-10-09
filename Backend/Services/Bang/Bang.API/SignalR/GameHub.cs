using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bang.API.SignalR
{
    public class GameHub : Hub<IGameHubClient>
    {

        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
