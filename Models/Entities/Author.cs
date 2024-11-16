using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace EcoTrack.Blog.Models.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
