using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.DAL.Domain;

namespace UserIdentity.BLL.Application.Interfaces.Hubs
{
    public interface ILobbyHub
    {
        Task SetMessage(Message message);
        Task SetMessages(List<Message> messages);
        Task RefreshLobbyUsers(long lobbyId);
    }
}
