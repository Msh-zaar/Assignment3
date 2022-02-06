using Assignment3.Models;
using Assignment3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly MovieDbContext _movieDbContext;

        public CharacterRepository(MovieDbContext context)
        {
            _movieDbContext = context;
        }

        public async Task<Character> AddCharacterAsync(Character character)
        {
            _movieDbContext.Characters.Add(character);
            await _movieDbContext.SaveChangesAsync();
            return character;
        }

        public bool CharacterExists(int id)
        {
            return _movieDbContext.Characters.Any(c => c.Id == id);
        }

        public async Task DeleteCharacterAsync(int id)
        {
            Character character = await _movieDbContext.Characters.FindAsync(id);
            _movieDbContext.Characters.Remove(character);
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _movieDbContext.Characters
                .Include(c => c.Movies)
                .ToListAsync();
        }

        public async Task<Character> GetSpecificCharacterAsync(int id)
        {
            return await _movieDbContext.Characters
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCharacterAsync(Character character)
        {
            _movieDbContext.Entry(character).State = EntityState.Modified;
            await _movieDbContext.SaveChangesAsync();
        }
    }
}
