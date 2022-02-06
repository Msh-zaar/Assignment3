using Assignment3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment3.Repositories.Interfaces
{
    public interface IMovieRepository
    {
		//CRUD
		public Task<Movie> GetSpecificMovieAsync(int id);
		public Task<IEnumerable<Movie>> GetAllMoviesAsync();
		public Task<Movie> AddMovieAsync(Movie movie);
		public Task UpdateMovieAsync(Movie movie);
		public Task DeleteMovieAsync(int id);

		//Extended CRUD
		public Task UpdateCharactersInMovie(int id, int[] characterIds);
		public Task<List<Character>> GetAllCharactersInMovie(int id);

		//Other
		public bool MovieExists(int id);
	}
}
