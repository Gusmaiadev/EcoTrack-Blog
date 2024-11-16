using EcoTrack.Blog.Models.DTOs;

namespace EcoTrack.Blog.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto> RegisterAsync(AuthorRegisterDto registerDto);
        Task<string> LoginAsync(AuthorLoginDto loginDto);
        Task<AuthorDto> GetByIdAsync(int id);
    }
}