using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PasswordGenerator;

namespace UserIdentity.BLL.Infrastructure.Stores
{
    public class LobbyStore : ILobbyStore
    {
        private readonly UserIdentityDbContext _dbContext;

        public LobbyStore(UserIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LobbyAccount>> GetLobbyAccountsAsync(long lobbyId, CancellationToken cancellationToken)
        {
            return await _dbContext.LobbyAccounts
                .Include(a => a.Account)
                .Where(la => la.LobbyId == lobbyId)
                .ToListAsync(cancellationToken);
        }

        public async Task CreateLobbyAccountAsync(string accountId, string password, CancellationToken cancellationToken)
        {
            var lobbyAccount = new LobbyAccount
            {
                AccountId = accountId,
                LobbyId = await GetLobbyIdByPasswordAsync(password, cancellationToken),
                IsConnected = true
            };

            await _dbContext.LobbyAccounts.AddAsync(lobbyAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteLobbyAccountAsync(string accountId, CancellationToken cancellationToken)
        {
            var lobbyAccountId = await GetLobbyAccountIdByAccountIdAsync(accountId, cancellationToken);

            _dbContext.LobbyAccounts.Remove(new LobbyAccount { Id = lobbyAccountId });
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> CreateLobbyAsync(string accountId, CancellationToken cancellationToken)
        {
            var password = await GeneratePasswordAsync();

            var lobby = new Lobby
            {
                OwnerId = accountId,
                Password = password
            };

            await _dbContext.Lobbies.AddAsync(lobby, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await CreateLobbyAccountAsync(accountId, password, cancellationToken);

            return password;
        }

        public async Task DeleteLobbyAsync(long id, CancellationToken cancellationToken)
        {
            var lobby = new Lobby
            {
                Id = id
            };

            _dbContext.Lobbies.Remove(lobby);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<long> GetLobbyIdByPasswordAsync(string password, CancellationToken cancellationToken)
        {
            var lobby = await _dbContext.Lobbies
                .Where(l => l.Password == password)
                .FirstOrDefaultAsync(cancellationToken);

            if(lobby == null) 
                throw new EntityNotFoundException("Lobby not found!");

            return lobby.Id;
        }

        private async Task<long> GetLobbyAccountIdByAccountIdAsync(string accountId, CancellationToken cancellationToken)
        {
            var lobby = await _dbContext.LobbyAccounts
                .Where(l => l.AccountId == accountId)
                .FirstOrDefaultAsync(cancellationToken);

            if (lobby == null)
                throw new EntityNotFoundException("Lobby not found!");

            return lobby.Id;
        }

        private async Task<string> GeneratePasswordAsync()
        {
            string password;
            do
            {
                password = new Password(includeLowercase: true, includeUppercase: false, includeNumeric: true, includeSpecial: false, passwordLength: 6).Next();
            } while (await _dbContext.Lobbies.AnyAsync(l => l.Password == password));

            return password;
        }
    }
}
