using EcoTrack.Blog.Models.DTOs;

namespace EcoTrack.Blog.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllAsync();
        Task<PostDto> GetByIdAsync(int id);
        Task<IEnumerable<PostDto>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<PostDto>> GetByAuthorIdAsync(int authorId);
        Task<PostDto> CreateAsync(int authorId, PostCreateDto createDto);
        Task<PostDto> UpdateAsync(int authorId, PostUpdateDto updateDto);
        Task DeleteAsync(int authorId, int postId);
    }
}