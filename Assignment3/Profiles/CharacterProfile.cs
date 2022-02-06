﻿using Assignment3.Models;
using Assignment3.Models.DTOs.Character;
using AutoMapper;
using System.Linq;

namespace Assignment3.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            //Read
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(crdto => crdto.Movies, opt => opt.MapFrom(c => c.Movies.Select(m => m.Id)));

            //Edit
            CreateMap<CharacterEditDTO, Character>();

            //Create
            CreateMap<CharacterCreateDTO, Character>();
        }
    }
}
