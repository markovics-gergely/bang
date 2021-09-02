using Bang.DAL.Domain;

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bang.BLL.Application.Interfaces
{
    public interface ICharacterStore
    {
        Task<Character> GetCharacterAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Character>> GetCharactersAsync(CancellationToken cancellationToken);
        Task<long> CreateCharacterAsync(Character character, CancellationToken cancellationToken);
        Task UpdateCharacterAsync(long id, Character character, CancellationToken cancellationToken);
        Task DeleteCharacterAsync(long id, CancellationToken cancellationToken);
    }
}
