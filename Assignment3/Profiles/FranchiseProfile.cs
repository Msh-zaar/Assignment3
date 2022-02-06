using Assignment3.Models;
using Assignment3.Models.DTOs.Franchise;
using AutoMapper;
using System.Linq;

namespace Assignment3.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            //Read
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(fdto => fdto.Movies, opt => opt
                .MapFrom(f => f.Movies
                .Select(m => m.Id)
                .ToList()));

            //Edit
            CreateMap<FranchiseEditDTO, Franchise>();

            //Create
            CreateMap<FranchiseCreateDTO, Franchise>();
        }
    }
}
