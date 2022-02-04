using Assignment3.Models;
using Assignment3.Models.DTOs.Franchise;
using Assignment3.Models.DTOs.Movie;
using AutoMapper;

namespace Assignment3.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadDTO>();
            CreateMap<MovieEditDTO, Movie>();
            CreateMap<MovieCreateDTO, Movie>();
        }
    }
}
