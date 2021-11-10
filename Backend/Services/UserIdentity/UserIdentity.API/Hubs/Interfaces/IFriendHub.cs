﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;
using UserIdentity.DAL.Domain;

namespace UserIdentity.API.Hubs.Interfaces
{
    public interface IFriendHub
    {
        Task SetFriendInvite(FriendViewModel account);
        Task SetFriendRequest(FriendViewModel account);
        Task SetFriend(FriendViewModel account);
    }
}
