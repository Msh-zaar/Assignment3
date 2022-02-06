using Assignment3.Models;
using Assignment3.Models.DTOs.Franchise;
using Assignment3.Models.DTOs.Movie;
using AutoMapper;
using System.Linq;

namespace Assignment3.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            //Read
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(mdto => mdto.Characters, opt => opt
                .MapFrom(m => m.Characters
                .Select(c => c.Id)
                .ToList()));

            //Map Movie to Franchise
            CreateMap<Movie, FranchiseMovieDTO>();

            //Edit
            CreateMap<MovieEditDTO, Movie>();

            //Create
            CreateMap<MovieCreateDTO, Movie>();
        }
    }
}
