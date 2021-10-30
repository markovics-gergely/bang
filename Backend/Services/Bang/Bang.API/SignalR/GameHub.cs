using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.API.SignalR
{
    public class GameHub : Hub<IGameHubClient>
    {
        public static ConcurrentDictionary<string, string> Connections { get; } = new ConcurrentDictionary<string, string>();

        public async static Task Refresh(IMediator mediator, IHubContext<GameHub, IGameHubClient> hub, CancellationToken cancellationToken)
        {
            foreach (var users in Connections)
            {
                var query = new GetGameBoardByUserSimplifiedQuery(users.Value);
                var perQuery = new GetPermissionsByUserQuery(users.Value);
                var gameboard = await mediator.Send(query, cancellationToken);
                var permissions = await mediator.Send(perQuery, cancellationToken);
                await hub.Clients.Client(users.Key).RefreshBoard(gameboard);
                await hub.Clients.Client(users.Key).RefreshPermission(permissions);
            }
        }

        public GameHub()
        {
        }

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userid"];
            Connections.TryAdd(GetConnectionId(), userId[0]);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Connections.TryRemove(GetConnectionId(), out _);
            return base.OnDisconnectedAsync(exception);
        }

        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
