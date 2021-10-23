using Bang.BLL.Infrastructure.Queries.ViewModels;
using System.Threading.Tasks;

namespace Bang.API.SignalR
{
    public interface IGameHubClient
    {
        Task RefreshBoard(GameBoardByUserViewModel gameBoard);
        Task AddToGroup(string groupName);
        Task RemoveFromGroup(string groupName);
    }
}
