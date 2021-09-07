using Bang.BLL.Application.Exceptions;
using Bang.BLL.Application.Interfaces;
using Bang.DAL;
using Bang.DAL.Domain.Catalog.Characters;

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

        public async Task<Character> GetCharacterAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbContext.Characters.Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Character not found!");
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Characters.ToListAsync(cancellationToken);
        }

        public async Task<long> CreateCharacterAsync(Character character, CancellationToken cancellationToken)
        {
            await _dbContext.Characters.AddAsync(character, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return character.Id;
        }

        public async Task UpdateCharacterAsync(long id, Character character, CancellationToken cancellationToken)
        {
            character.Id = id;
            var entry = _dbContext.Attach(character);
            entry.State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == id, cancellationToken) == null)
                    throw new EntityNotFoundException("Character not found!");
                else throw;
            }
        }

        public async Task DeleteCharacterAsync(long id, CancellationToken cancellationToken)
        {
            _dbContext.Characters.Remove(new Character() { Id = id });

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _dbContext.Characters.FirstOrDefaultAsync(p => p.Id == id, cancellationToken) == null)
                    throw new EntityNotFoundException("Character not found!");
                else throw;
            }
        }
    }
}
