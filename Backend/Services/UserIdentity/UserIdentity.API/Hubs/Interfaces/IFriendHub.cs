using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.DAL.Domain;

namespace UserIdentity.API.Hubs.Interfaces
{
    public interface IFriendHub
    {
        Task SetFriendInvite(Account account);
        Task SetFriendRequest(Account account);
        Task SetFriend(Account account);
    }
}
