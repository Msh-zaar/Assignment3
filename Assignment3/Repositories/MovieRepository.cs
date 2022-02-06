using Assignment3.Models;
using Assignment3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _movieDbContext;
        public MovieRepository(MovieDbContext context)
        {
            _movieDbContext = context;
        }
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _movieDbContext.Movies.Add(movie);
            await _movieDbContext.SaveChangesAsync();

            return movie;
        }
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _movieDbContext.Movies.FindAsync(id);
            _movieDbContext.Movies.Remove(movie);

            await _movieDbContext.SaveChangesAsync();
        }

        public async Task<List<Character>> GetAllCharactersInMovie(int id)
        {
            Movie movie = await _movieDbContext.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);

            List<Character> movieCharacters = new();
            foreach (Character character in movie.Characters)
            {
                movieCharacters.Add(character);
            }
            return movieCharacters;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movieDbContext.Movies.Include(m => m.Characters).ToListAsync();
        }

        public async Task<Movie> GetSpecificMovieAsync(int id)
        {
            return await _movieDbContext.Movies
                .Include(movie => movie.Characters)
                .FirstOrDefaultAsync(movie => movie.Id == id);
        }

        public bool MovieExists(int id)
        {
            return _movieDbContext.Movies.Any(m => m.Id == id);
        }

        public async Task UpdateCharactersInMovie(int id, int[] characterIds)
        {
            Movie movie = await _movieDbContext.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);

            List<Character> characterList = new();
            foreach (int characterId in characterIds)
            {
                Character character = _movieDbContext.Characters.Where(c => c.Id == characterId).FirstOrDefault();
                characterList.Add(character);
            }
            movie.Characters = characterList;

            _movieDbContext.Entry(movie).State = EntityState.Modified;
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _movieDbContext.Entry(movie).State = EntityState.Modified;
            await _movieDbContext.SaveChangesAsync();
        }
    }
}
