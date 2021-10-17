using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.DAL.Domain;

namespace UserIdentity.BLL.Application.Interfaces.Hubs
{
    public interface IChatHub
    {
        Task RoomCreated(Room room);
        Task RoomAbandoned(string roomName);
        Task UserEntered(Account account);
        Task UserLeft(string userId);
        Task RecieveMessage(Message message);
        Task SetUsers(List<Account> users);
        Task SetMessages(List<Message> messages);
    }
}
