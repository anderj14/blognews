using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using AutoMapper;

namespace API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Status, StatusDto>();
            CreateMap<AppUser, UserDto>();
        }
    }
}