namespace EcoTrack.Blog.Models.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryCreateDto
    {
        public string Name { get; set; }
    }
}