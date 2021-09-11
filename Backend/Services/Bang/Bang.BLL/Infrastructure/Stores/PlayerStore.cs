﻿using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Bang.BLL.Infrastructure.Stores
{
    public class PlayerStore : IPlayerStore
    {
        private readonly BangDbContext _dbContext;

        public PlayerStore(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Player> GetPlayerAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Card not found!");
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Players.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Player>> GetPlayersByGameBoardAsync(long gameBoardId, CancellationToken cancellationToken)
        {
            return await _dbContext.Players.Where(p => p.GameBoardId == gameBoardId).ToListAsync(cancellationToken);

        }
    }
}