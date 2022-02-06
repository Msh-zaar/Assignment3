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
    [Route("api/[controller]")]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<CharacterReadDTO>> GetAllCharacters()
        {
            var characters = await _repository.GetAllCharactersAsync();
            var charReadDTO = _mapper.Map<List<CharacterReadDTO>>(characters);

            return charReadDTO;
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
        {
            var character = await _repository.GetSpecificCharacterAsync(id);

            if (character == null)
                return NotFound();

            CharacterReadDTO charReadDTO = _mapper.Map<CharacterReadDTO>(character);

            return charReadDTO;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO charCreateDTO)
        {
            Character character = _mapper.Map<Character>(charCreateDTO);
            character = await _repository.AddCharacterAsync(character);

            return CreatedAtAction("GetCharacter", new { id = character.Id }, _mapper.Map<CharacterCreateDTO>(character));
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!_repository.CharacterExists(id))
                return NotFound();

            await _repository.DeleteCharacterAsync(id);

            return NoContent();
        }
    }
}
