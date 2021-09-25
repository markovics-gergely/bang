using Microsoft.AspNetCore.SignalR;

namespace Bang.API.SignalR
{
    public class GameHub : Hub<IGameHubClient>
    {
    }
}
