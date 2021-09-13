using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface ICharacterStore
    {
        Task<Character> GetCharacterByTypeAsync(CharacterType type, CancellationToken cancellationToken);
        Task<IEnumerable<Character>> GetCharactersAsync(CancellationToken cancellationToken);
    }
}
