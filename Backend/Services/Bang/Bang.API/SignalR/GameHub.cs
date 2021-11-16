using Bang.BLL.Application.Interfaces;
using Bang.BLL.Infrastructure.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bang.DAL.Domain.Constants.Enums;

namespace Bang.API.SignalR
{
    public class GameHub : Hub<IGameHubClient>
    {
        private readonly IGameBoardStore _gameBoardStore;

        public GameHub(IGameBoardStore gameBoardStore)
        {
            _gameBoardStore = gameBoardStore;
        }

        private static ConcurrentDictionary<string, PlayerData> Connections { get; } = new ConcurrentDictionary<string, PlayerData>();
        private static ConcurrentDictionary<string, GroupData> GameGroups { get; } = new ConcurrentDictionary<string, GroupData>();

        private class PlayerData
        {
            public string UserId { get; set; }
            public string LobbyOwnerId { get; set; }
        }

        private class GroupPlayerData
        {
            public string UserId { get; set; }
            public string ConnectionId { get; set; }
        }

        private class GroupData
        {
            public List<GroupPlayerData> GroupConnections { get; set; } = new List<GroupPlayerData>(); 
        }

        public async static Task Refresh(IMediator mediator, IHubContext<GameHub, IGameHubClient> hub, CancellationToken cancellationToken)
        {
            var ownerQuery = new GetGameBoardOwnerByUserQuery();
            var ownerId = await mediator.Send(ownerQuery, cancellationToken);
            GameGroups.TryGetValue(ownerId, out GroupData group);
            if (group != null)
            {
                foreach (var users in group.GroupConnections)
                {
                    var query = new GetGameBoardByUserSimplifiedQuery(users.UserId);
                    var perQuery = new GetPermissionsByUserQuery(users.UserId);
                    var gameboard = await mediator.Send(query, cancellationToken);
                    var permissions = await mediator.Send(perQuery, cancellationToken);
                    _ = hub.Clients.Client(users.ConnectionId).RefreshBoard(gameboard);
                    _ = hub.Clients.Client(users.ConnectionId).RefreshPermission(permissions);
                }
            }
        }

        public void GameDeleted(string groupName, NavigateEnum navigate)
        {
            _ = Clients.Group(groupName).GameDeleted(navigate);
        }

        public override async Task<Task> OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userid"][0];
            var ownerId = await _gameBoardStore.GetGameBoardOwnerByUserAsync(userId, new CancellationToken());
            var playerData = new PlayerData()
            {
                UserId = userId,
                LobbyOwnerId = ownerId
            };
            if (GameGroups.ContainsKey(ownerId))
            {
                GameGroups.TryGetValue(ownerId, out GroupData group);
                var groupPlayerData = new GroupPlayerData()
                {
                    UserId = userId,
                    ConnectionId = GetConnectionId()
                };
                group.GroupConnections.Add(groupPlayerData);
            }
            else
            {
                var group = new GroupData();
                var groupPlayerData = new GroupPlayerData()
                {
                    UserId = userId,
                    ConnectionId = GetConnectionId()
                };
                group.GroupConnections.Add(groupPlayerData);
                GameGroups.TryAdd(ownerId, group);
            }
            await Groups.AddToGroupAsync(GetConnectionId(), ownerId);

            Connections.TryAdd(GetConnectionId(), playerData);
            return base.OnConnectedAsync();
        }

        public override async Task<Task> OnDisconnectedAsync(Exception exception)
        {
            Connections.TryRemove(GetConnectionId(), out PlayerData data);
            GameGroups.TryGetValue(data.LobbyOwnerId, out GroupData group);
            var deletable = group.GroupConnections.FirstOrDefault(g => g.ConnectionId == GetConnectionId());
            group.GroupConnections.Remove(deletable);
            if (group.GroupConnections.Count == 0)
            {
                GameGroups.TryRemove(data.LobbyOwnerId, out _);
            }
            await Groups.RemoveFromGroupAsync(GetConnectionId(), data.LobbyOwnerId);
            return base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
