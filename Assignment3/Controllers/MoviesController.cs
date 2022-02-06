using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using AutoMapper;
using Assignment3.Repositories.Interfaces;
using Assignment3.Models.DTOs.Movie;

namespace Assignment3.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Movies
        /// </summary>
        /// <returns>List of Movies</returns>
        [HttpGet]
        public async Task<IEnumerable<MovieReadDTO>> GetMovies()
        {
            IEnumerable<Movie> movieList = await _repository.GetAllMoviesAsync();
            List<MovieReadDTO> movieReadDTOList = _mapper.Map<List<MovieReadDTO>>(movieList);

            return movieReadDTOList;
        }

        /// <summary>
        /// Get specified Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Movie</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
        {
            if (!_repository.MovieExists(id))
                return NotFound();

            Movie movie = await _repository.GetSpecificMovieAsync(id);
            MovieReadDTO movieReadDTO = _mapper.Map<MovieReadDTO>(movie);

            return movieReadDTO;
        }

        /// <summary>
        /// Get all characters from specific movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of Characters</returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<MovieCharacterDTO>>> GetMovieCharacters(int id)
        {
            if (!_repository.MovieExists(id))
                return NotFound();

            List<Character> movieChar = await _repository.GetAllCharactersInMovie(id);
            List<MovieCharacterDTO> movieCharDTO = _mapper.Map<List<MovieCharacterDTO>>(movieChar);

            return movieCharDTO;
        }

        /// <summary>
        /// Update a specified Movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieEditDTO"></param>
        /// <returns>NoContentResult object for response</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieEditDTO movieEditDTO)
        {
            if (id != movieEditDTO.Id)
                return BadRequest();

            if (!_repository.MovieExists(id))
                return NotFound();

            Movie domainMovie = _mapper.Map<Movie>(movieEditDTO);
            await _repository.UpdateMovieAsync(domainMovie);

            return NoContent();
        }

        /// <summary>
        /// Update a specified Movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characterIds"></param>
        /// <returns>NoContentResult object for response</returns>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> PutMovieCharacters(int id, int[] characterIds)
        {
            if (!_repository.MovieExists(id))
                return NotFound();

            await _repository.UpdateCharactersInMovie(id, characterIds);

            return NoContent();
        }

        /// <summary>
        /// Create a new Movie
        /// </summary>
        /// <param name="movieEditDTO"></param>
        /// <returns>CreatedAtAction object that produces a 201 status code</returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieEditDTO movieEditDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieEditDTO);
            movie = await _repository.AddMovieAsync(movie);

            return CreatedAtAction("GetMovie", new { id = movie.Id }, _mapper.Map<MovieReadDTO>(movie));
        }

        /// <summary>
        /// (Safely) Deletes a specified Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContentResult object for response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!_repository.MovieExists(id))
                return NotFound();

            await _repository.DeleteMovieAsync(id);

            return NoContent();
        }
    }
}
