namespace EcoTrack.Blog.Models.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class AuthorLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthorRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}