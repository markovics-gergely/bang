﻿using UserIdentity.DAL.Domain;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserIdentity.BLL.Application.Interfaces
{
    public interface ILobbyStore
    {
        Task<IEnumerable<LobbyAccount>> GetLobbyAccountsAsync(long lobbyId, CancellationToken cancellationToken);
        Task CreateLobbyAccountAsync(string accountId, string password, CancellationToken cancellationToken);
        Task DeleteLobbyAccountAsync(string accountId, CancellationToken cancellationToken);
        Task<string> CreateLobbyAsync(string accountId, CancellationToken cancellationToken);
        Task DeleteLobbyAsync(long id, CancellationToken cancellationToken);
    }
}
