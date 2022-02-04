using Assignment3.Models;
using Assignment3.Models.DTOs.Franchise;
using AutoMapper;

namespace Assignment3.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>();
            CreateMap<FranchiseEditDTO, Franchise>();
            CreateMap<FranchiseCreateDTO, Franchise>();
        }
    }
}
