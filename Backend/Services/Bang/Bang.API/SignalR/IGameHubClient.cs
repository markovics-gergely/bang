using Bang.BLL.Infrastructure.Queries.ViewModels;
using Bang.DAL.Domain.Constants.Enums;
using System.Threading.Tasks;

namespace Bang.API.SignalR
{
    public interface IGameHubClient
    {
        Task RefreshBoard(GameBoardByUserViewModel gameBoard);
        Task RefreshPermission(PermissionViewModel permission);
        Task GameDeleted(NavigateEnum navigateTo);
    }
}
