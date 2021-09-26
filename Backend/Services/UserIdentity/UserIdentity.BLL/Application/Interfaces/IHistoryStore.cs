﻿using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Interfaces
{
    public interface IFriendStore
    {
        Task<IEnumerable<Friend>> GetFriendsAsync(string ownId, CancellationToken cancellationToken);
        Task CreateFriendAsync(string ownId, string friendId, CancellationToken cancellationToken);
        Task DeleteFriendAsync(string ownId, string friendId, CancellationToken cancellationToken);
    }
}
