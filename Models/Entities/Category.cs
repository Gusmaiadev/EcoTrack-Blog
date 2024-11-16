using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace EcoTrack.Blog.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}