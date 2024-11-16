using AutoMapper;
using EcoTrack.Blog.Models.Entities;
using EcoTrack.Blog.Models.DTOs;

namespace EcoTrack.Blog.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Author Mappings
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorRegisterDto, Author>();

            // Category Mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();

            // Post Mappings
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<PostCreateDto, Post>();
            CreateMap<PostUpdateDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignora o Id durante a atualização
        }
    }
}