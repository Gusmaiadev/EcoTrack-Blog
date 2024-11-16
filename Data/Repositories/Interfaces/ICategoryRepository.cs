using EcoTrack.Blog.Models.Entities;

namespace EcoTrack.Blog.Data.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> NameExistsAsync(string name);
    }
}