using EcoTrack.Blog.Data.Repositories;
using EcoTrack.Blog.Data.Repositories.Interfaces;
using EcoTrack.Blog.Services;
using EcoTrack.Blog.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrack.Blog.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            // Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPostService, PostService>();

            return services;
        }
    }
}