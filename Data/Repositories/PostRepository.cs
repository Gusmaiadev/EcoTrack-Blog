﻿using EcoTrack.Blog.Data.Context;
using EcoTrack.Blog.Data.Repositories.Interfaces;
using EcoTrack.Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoTrack.Blog.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context) { }

        // Override do método GetByIdAsync para incluir Author e Category
        public override async Task<Post> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Author)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Author)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByAuthorIdAsync(int authorId)
        {
            return await _dbSet
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.AuthorId == authorId)
                .ToListAsync();
        }
    }
}