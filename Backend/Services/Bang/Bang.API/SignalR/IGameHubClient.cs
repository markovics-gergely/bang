using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bang.API.SignalR
{
    public interface IGameHubClient
    {
        Task GetGameBoard(string userid);
        Task AddToGroup(string groupName);
        Task RemoveFromGroup(string groupName);
    }
}
