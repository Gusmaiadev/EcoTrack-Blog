using EcoTrack.Blog.Data.Context;
using EcoTrack.Blog.Data.Repositories.Interfaces;
using EcoTrack.Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.Blog.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(c => c.Name == name);
        }
    }
}