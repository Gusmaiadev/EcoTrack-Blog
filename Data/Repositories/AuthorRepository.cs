using EcoTrack.Blog.Data.Context;
using EcoTrack.Blog.Data.Repositories.Interfaces;
using EcoTrack.Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.Blog.Data.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Author> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(a => a.Email == email);
        }
    }
}