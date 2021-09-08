﻿using Bang.DAL.Domain.Catalog;
using Bang.DAL.Domain.Constants.Enums;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bang.BLL.Application.Interfaces
{
    public interface ICharacterStore
    {
        Task<Character> GetCharacterAsync(int id, CancellationToken cancellationToken);
        Task<Character> GetCharacterByTypeAsync(CharacterType type, CancellationToken cancellationToken);
        Task<IEnumerable<Character>> GetCharactersAsync(CancellationToken cancellationToken);
        // TODO Feles
        public Task<int> CreateCharacterAsync(Character character, CancellationToken cancellationToken);
        public Task UpdateCharacterAsync(int id, Character character, CancellationToken cancellationToken);
        public Task DeleteCharacterAsync(int id, CancellationToken cancellationToken);
    }
}
