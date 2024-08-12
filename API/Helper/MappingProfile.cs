using API.CreateDtos;
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
            CreateMap<Article, ArticleDto>()
            .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.StatusName))
            .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.AuthorUserName, o => o.MapFrom(s => s.AppUser.UserName));

            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.AppUser.UserName));

            //Create
            CreateMap<ArticleCreateDto, Article>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<StatusCreateDto, Status>();
        }
    }
}