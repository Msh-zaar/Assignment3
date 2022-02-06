using Assignment3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment3.Repositories.Interfaces
{
    public interface IFranchiseRepository
    {
		//CRUD
		public Task<Franchise> GetSpecificFranchiseAsync(int id);
		public Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
		public Task<Franchise> AddFranchiseAsync(Franchise franchise);
		public Task UpdateFranchiseAsync(Franchise franchise);
		public Task DeleteFranchiseAsync(int id);

		//Extended CRUD
		public Task UpdateMoviesInFranchise(int id, int[] movieIds);
		public Task<List<Movie>> GetAllMoviesInFranchise(int id);
		public Task<List<Character>> GetAllCharactersInFranchise(int id);

		//Other
		public bool FranchiseExists(int id);
	}
}
