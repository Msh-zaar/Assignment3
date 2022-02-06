using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using Assignment3.Repositories.Interfaces;
using AutoMapper;
using Assignment3.Models.DTOs.Franchise;

namespace Assignment3.Controllers
{
    [Route("api/franchises")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseRepository _repository;
        private readonly IMapper _mapper;

        public FranchisesController(IFranchiseRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Franchises
        /// </summary>
        /// <returns>List of Franchises</returns>
        [HttpGet]
        public async Task<IEnumerable<FranchiseReadDTO>> GetFranchises()
        {
            IEnumerable<Franchise> franList = await _repository.GetAllFranchisesAsync();
            IEnumerable<FranchiseReadDTO> franReadDTOList = _mapper.Map<List<FranchiseReadDTO>>(franList);

            return franReadDTOList;
        }

        /// <summary>
        /// Get specified Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Franchise</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            Franchise franchise = await _repository.GetSpecificFranchiseAsync(id);

            if (franchise == null)
                return NotFound();

            FranchiseReadDTO franReadDTO = _mapper.Map<FranchiseReadDTO>(franchise);

            return franReadDTO;
        }

        /// <summary>
        /// Get all Movies of a specified Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of Movies</returns>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<FranchiseMovieDTO>>> GetFranchiseMovies(int id)
        {
            if (!_repository.FranchiseExists(id))
                return NotFound();

            List<Movie> franMoviesList = await _repository.GetAllMoviesInFranchise(id);
            List<FranchiseMovieDTO> franMoviesDTOList = _mapper.Map<List<FranchiseMovieDTO>>(franMoviesList);

            return franMoviesDTOList;
        }
        /// <summary>
        /// Get all Characters of a specified Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of Characters</returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<FranchiseCharacterDTO>>> GetFranchiseCharacters(int id)
        {
            if (!_repository.FranchiseExists(id))
                return NotFound();

            List<Character> franCharacterList = await _repository.GetAllCharactersInFranchise(id);
            List<FranchiseCharacterDTO> franCharacterDTOList = _mapper.Map<List<FranchiseCharacterDTO>>(franCharacterList);

            return franCharacterDTOList;
        }

        /// <summary>
        /// Update a specified Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franEditDTO"></param>
        /// <returns>NoContentResult object for response</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseEditDTO franEditDTO)
        {
            if (id != franEditDTO.Id)
                return BadRequest();

            if (!_repository.FranchiseExists(id))
                return NotFound();

            Franchise domainFranchise = _mapper.Map<Franchise>(franEditDTO);
            await _repository.UpdateFranchiseAsync(domainFranchise);

            return NoContent();
        }

        /// <summary>
        /// Create a new Franchise
        /// </summary>
        /// <param name="franCreateDTO"></param>
        /// <returns>CreatedAtAction object that produces a 201 status code</returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO franCreateDTO)
        {
            Franchise franchise = _mapper.Map<Franchise>(franCreateDTO);

            franchise = await _repository.AddFranchiseAsync(franchise);

            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, _mapper.Map<FranchiseReadDTO>(franchise));
        }

        /// <summary>
        /// (Safely) Deletes a specified Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContentResult object for response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (!_repository.FranchiseExists(id))
                return NotFound();

            await _repository.DeleteFranchiseAsync(id);

            return NoContent();
        }
    }
}
