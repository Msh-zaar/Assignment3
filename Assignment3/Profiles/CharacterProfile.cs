using Assignment3.Models;
using Assignment3.Models.DTOs.Character;
using AutoMapper;

namespace Assignment3.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>();
            CreateMap<CharacterEditDTO, Character>();
            CreateMap<CharacterCreateDTO, Character>();
        }
    }
}
