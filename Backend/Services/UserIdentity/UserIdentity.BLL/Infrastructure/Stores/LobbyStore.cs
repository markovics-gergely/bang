using UserIdentity.BLL.Application.Exceptions;
using UserIdentity.BLL.Application.Interfaces;
using UserIdentity.DAL;
using UserIdentity.DAL.Domain;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PasswordGenerator;

namespace UserIdentity.BLL.Infrastructure.Stores
{
    public class LobbyStore : ILobbyStore
    {
        private readonly UserIdentityDbContext _dbContext;
        private readonly IAccountStore _accountStore;

        public LobbyStore(UserIdentityDbContext dbContext, IAccountStore accountStore)
        {
            _dbContext = dbContext;
            _accountStore = accountStore;
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

        public async Task DeleteLobbyAccountAsync(long lobbyId, string accountId, CancellationToken cancellationToken)
        {
            var actualLobby = await _dbContext.Lobbies.Where(l => l.Id == lobbyId).FirstOrDefaultAsync(cancellationToken);
            var kickableAcc = await _dbContext.LobbyAccounts.Where(la => la.AccountId == accountId).FirstOrDefaultAsync(cancellationToken);
            var actualAccId = _accountStore.GetActualAccountId();

            if (actualLobby == null || kickableAcc == null)
                throw new EntityNotFoundException("Lobby or player not found!");

            var playersOfLobby = (await GetLobbyAccountsAsync(lobbyId, cancellationToken)).ToList();

            if (playersOfLobby.Count() == 1)
            {
                await DeleteLobbyAsync(kickableAcc.LobbyId, cancellationToken);
                return;
            }
            else
            {
                _dbContext.LobbyAccounts.Remove(kickableAcc);
                await _dbContext.SaveChangesAsync(cancellationToken);

                if (actualAccId == actualLobby.OwnerId)
                {
                    actualLobby.OwnerId = playersOfLobby.FirstOrDefault().AccountId;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }
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

        private async Task DeleteLobbyAsync(long id, CancellationToken cancellationToken)
        {
            var lobby = await _dbContext.Lobbies.Where(l => l.Id == id).FirstOrDefaultAsync(cancellationToken);

            _dbContext.Lobbies.Remove(lobby);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<long> GetLobbyIdByPasswordAsync(string password, CancellationToken cancellationToken)
        {
            var lobby = await _dbContext.Lobbies
                .Where(l => l.Password == password)
                .FirstOrDefaultAsync(cancellationToken);

            if(lobby == null)
            {
                throw new EntityNotFoundException("Lobby not found!");
            }

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
