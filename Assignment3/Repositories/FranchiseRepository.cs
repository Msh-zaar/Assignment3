using Assignment3.Models;
using Assignment3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Repositories
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly MovieDbContext _movieDbContext;

        public FranchiseRepository(MovieDbContext context)
        {
            _movieDbContext = context;
        }
        public async Task<Franchise> AddFranchiseAsync(Franchise franchise)
        {
            _movieDbContext.Franchises.Add(franchise);
            await _movieDbContext.SaveChangesAsync();
            return franchise;
        }

        public async Task DeleteFranchiseAsync(int id)
        {
            Franchise franchise = await _movieDbContext.Franchises.FindAsync(id);
            var moviesInFranchise = _movieDbContext.Movies.Where(m => m.FranchiseId == id);

            //Safe Delete
            foreach (var m in moviesInFranchise)
            {
                m.FranchiseId = null;
            }
            _movieDbContext.Franchises.Remove(franchise);
            await _movieDbContext.SaveChangesAsync();
        }

        public bool FranchiseExists(int id)
        {
            return _movieDbContext.Franchises.Any(f => f.Id == id);
        }

        public async Task<List<Character>> GetAllCharactersInFranchise(int id)
        {
            Franchise franchise = await _movieDbContext.Franchises
                .Include(f => f.Movies)
                .ThenInclude(m => m.Characters)
                .FirstOrDefaultAsync(f => f.Id == id);

            List<Character> franCharList = new();

            foreach (Movie movie in franchise.Movies)
            {
                foreach (Character character in movie.Characters)
                {
                    franCharList.Add(character);
                }
            }

            return franCharList;
        }

        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _movieDbContext.Franchises.Include(f => f.Movies).ToListAsync();
        }

        public async Task<List<Movie>> GetAllMoviesInFranchise(int id)
        {
            Franchise franchise = await _movieDbContext.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == id);

            List<Movie> franMovieList = new();

            foreach (Movie movie in franchise.Movies)
            {
                franMovieList.Add(movie);
            }

            return franMovieList;
        }

        public async Task<Franchise> GetSpecificFranchiseAsync(int id)
        {
            return await _movieDbContext.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            _movieDbContext.Entry(franchise).State = EntityState.Modified;
            await _movieDbContext.SaveChangesAsync();
        }

        public async Task UpdateMoviesInFranchise(int id, int[] movieIds)
        {
            Franchise franchise = await _movieDbContext.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);

            List<Movie> editMovieList = new();

            foreach (int movieId in movieIds)
            {

            }
        }
    }
}
