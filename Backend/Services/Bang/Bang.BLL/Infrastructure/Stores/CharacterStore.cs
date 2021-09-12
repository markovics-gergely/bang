using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.EntityFrameworkCore;

namespace Bang.BLL.Infrastructure.Stores
{
    public class CharacterStore : ICharacterStore
    {
        private readonly BangDbContext _dbContext;

        public CharacterStore(BangDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Character> GetCharacterByTypeAsync(CharacterType type, CancellationToken cancellationToken)
        {
            return await _dbContext.Characters.Where(c => c.CharacterType == type).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Character not found!");
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Characters.ToListAsync(cancellationToken);
        }
    }
}
