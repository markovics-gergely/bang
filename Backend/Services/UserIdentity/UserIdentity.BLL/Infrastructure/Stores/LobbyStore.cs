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
using UserIdentity.BLL.Infrastructure.Queries.ViewModels;

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

        public async Task<Lobby> GetActualLobbyAsync(string accountId, CancellationToken cancellationToken)
        {
            var lobbyId = await _dbContext.LobbyAccounts
                .Where(la => la.AccountId == accountId)
                .Select(l => l.LobbyId)
                .FirstOrDefaultAsync(cancellationToken);

            if(lobbyId == null)
            {
                return null;  
            }

            return await _dbContext.Lobbies
                .Where(l => l.Id == lobbyId)
                .Include(owner => owner.Owner)
                .FirstOrDefaultAsync(cancellationToken);
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
            var lobbyId = await GetLobbyIdByPasswordAsync(password, cancellationToken);

            if (_dbContext.LobbyAccounts.Where(la => la.LobbyId == lobbyId).Count() >= 7)
            {
                throw new InvalidActionException("Lobby is full!");
            }

            if (await _dbContext.Lobbies.Where(la => la.Id == lobbyId).Select(s => s.GameBoardId).FirstOrDefaultAsync(cancellationToken) != 0)
            {
                throw new InvalidActionException("The game has started!");
            }

            if (_dbContext.LobbyAccounts.Where(la => la.LobbyId == lobbyId).Any(user => user.AccountId == accountId))
            {
                throw new InvalidActionException("You are already in this lobby!");
            }

            var removableAcc = await _dbContext.LobbyAccounts.Where(la => la.AccountId == accountId).FirstOrDefaultAsync(cancellationToken);
            if (removableAcc != null)
            {
                await DeleteLobbyAccountAsync(removableAcc.LobbyId, removableAcc.AccountId, cancellationToken);
            }

            var lobbyAccount = new LobbyAccount
            {
                AccountId = accountId,
                LobbyId = lobbyId,
                IsConnected = true
            };

            await _dbContext.LobbyAccounts.AddAsync(lobbyAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteLobbyAccountAsync(long lobbyId, string accountId, CancellationToken cancellationToken)
        {
            var actualLobby = await _dbContext.Lobbies.Where(l => l.Id == lobbyId).FirstOrDefaultAsync(cancellationToken);
            var kickableAcc = await _dbContext.LobbyAccounts.Where(la => la.AccountId == accountId).FirstOrDefaultAsync(cancellationToken);

            if (actualLobby == null || kickableAcc == null)
            {
                throw new EntityNotFoundException("Lobby or player not found!");
            }

            var playersOfLobby = (await GetLobbyAccountsAsync(lobbyId, cancellationToken)).ToList();

            if (playersOfLobby.Count() == 1)
            {
                await DeleteLobbyAccountsAsync(kickableAcc.LobbyId, cancellationToken);
                await DeleteLobbyAsync(kickableAcc.LobbyId, cancellationToken);
            }
            else
            {
                _dbContext.LobbyAccounts.Remove(kickableAcc);
                await _dbContext.SaveChangesAsync(cancellationToken);
                playersOfLobby.Remove(kickableAcc);

                if (accountId == actualLobby.OwnerId)
                {
                    actualLobby.OwnerId = playersOfLobby.FirstOrDefault().AccountId;
                    await UpdateLobbyAsync(actualLobby, cancellationToken);
                }
            }
        }

        public async Task<long> CreateLobbyAsync(string accountId, CancellationToken cancellationToken)
        {
            if (await _dbContext.LobbyAccounts.AnyAsync(la => la.AccountId == accountId))
            {
                throw new InvalidActionException("You can't create a lobby!");
            }

            var password = await GeneratePasswordAsync();

            var lobby = new Lobby
            {
                OwnerId = accountId,
                Password = password
            };

            await _dbContext.Lobbies.AddAsync(lobby, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await CreateLobbyAccountAsync(accountId, password, cancellationToken);

            return lobby.Id;
        }

        public async Task<string> GetPasswordByAccountIdAsync(string accountId, CancellationToken cancellationToken)
        {
            var lobbyId = (await _dbContext.LobbyAccounts.Where(la => la.AccountId == accountId).FirstOrDefaultAsync(cancellationToken)).LobbyId;

            return (await _dbContext.Lobbies.Where(la => la.Id == lobbyId).FirstOrDefaultAsync(cancellationToken)).Password;
        }

        private async Task DeleteLobbyAsync(long id, CancellationToken cancellationToken)
        {
            var lobby = await _dbContext.Lobbies.Where(l => l.Id == id).FirstOrDefaultAsync(cancellationToken);

            _dbContext.Lobbies.Remove(lobby);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Lobby> GetLobbyByIdAsync(long lobbyId, CancellationToken cancellationToken)
        {
            return await _dbContext.Lobbies.Where(l => l.Id == lobbyId).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Lobby not found");
        }

        public async Task<Lobby> GetLobbyByOwnerIdAsync(string ownerId, CancellationToken cancellationToken)
        {
            return await _dbContext.Lobbies.Where(l => l.OwnerId == ownerId).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Lobby not found");
        }

        public async Task UpdateLobbyAsync(Lobby lobby, CancellationToken cancellationToken)
        {
            _dbContext.Attach(lobby);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task DeleteLobbyAccountsAsync(long lobbyId, CancellationToken cancellationToken)
        {
            var lobbyAccounts = await _dbContext.LobbyAccounts.Where(la => la.LobbyId == lobbyId).ToListAsync(cancellationToken);

            _dbContext.LobbyAccounts.RemoveRange(lobbyAccounts);
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
