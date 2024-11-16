using EcoTrack.Blog.Models.DTOs;

namespace EcoTrack.Blog.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto createDto);
        Task<CategoryDto> UpdateAsync(int id, CategoryCreateDto updateDto);
        Task DeleteAsync(int id);
    }
}