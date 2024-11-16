using EcoTrack.Blog.Models.Entities;

namespace EcoTrack.Blog.Data.Repositories.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Post>> GetByAuthorIdAsync(int authorId);
    }
}