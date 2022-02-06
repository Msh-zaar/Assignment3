using Assignment3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment3.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        //CRUD
        public Task<Character> GetSpecificCharacterAsync(int id);
        public Task<IEnumerable<Character>> GetAllCharactersAsync();
        public Task<Character> AddCharacterAsync(Character character);
        public Task UpdateCharacterAsync(Character character);
        public Task DeleteCharacterAsync(int id);

        //Other
        public bool CharacterExists(int id);
    }
}
