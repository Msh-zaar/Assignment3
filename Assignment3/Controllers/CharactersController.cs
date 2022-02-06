using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using Assignment3.Repositories.Interfaces;
using Assignment3.Models.DTOs.Character;
using AutoMapper;

namespace Assignment3.Controllers
{
    [Route("api/characters")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _repository;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Characters
        /// </summary>
        /// <returns>List of Characters</returns>
        [HttpGet]
        public async Task<IEnumerable<CharacterReadDTO>> GetCharacters()
        {
            IEnumerable<Character> characters = await _repository.GetAllCharactersAsync();
            List<CharacterReadDTO> charReadDTO = _mapper.Map<List<CharacterReadDTO>>(characters);

            return charReadDTO;
        }

        /// <summary>
        /// Get specified Character
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Character</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
        {
            var character = await _repository.GetSpecificCharacterAsync(id);

            if (character == null)
                return NotFound();

            CharacterReadDTO charReadDTO = _mapper.Map<CharacterReadDTO>(character);

            return charReadDTO;
        }

        /// <summary>
        /// Update a specified Character
        /// </summary>
        /// <param name="id"></param>
        /// <param name="charEditDTO"></param>
        /// <returns>NoContentResult object for response</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterEditDTO charEditDTO)
        {
            if (id != charEditDTO.Id)
            {
                return BadRequest();
            }

            if (!_repository.CharacterExists(id))
                return NotFound();

            Character domainCharacter = _mapper.Map<Character>(charEditDTO);
            await _repository.UpdateCharacterAsync(domainCharacter);

            return NoContent();
        }

        /// <summary>
        /// Create a new Character
        /// </summary>
        /// <param name="charCreateDTO"></param>
        /// <returns>CreatedAtAction object that produces a 201 status code</returns>
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO charCreateDTO)
        {
            Character character = _mapper.Map<Character>(charCreateDTO);
            character = await _repository.AddCharacterAsync(character);

            return CreatedAtAction("GetCharacter", new { id = character.Id }, _mapper.Map<CharacterCreateDTO>(character));
        }

        /// <summary>
        /// (Safely) Deletes a specified Character
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContentResult object for response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!_repository.CharacterExists(id))
                return NotFound();

            await _repository.DeleteCharacterAsync(id);

            return NoContent();
        }
    }
}
