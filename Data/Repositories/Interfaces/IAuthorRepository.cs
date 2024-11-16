using EcoTrack.Blog.Models.Entities;

namespace EcoTrack.Blog.Data.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<Author> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}