using EcoTrack.Blog.Models.Entities;

namespace EcoTrack.Blog.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Author author);
    }
}