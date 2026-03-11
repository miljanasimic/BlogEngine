using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public List<Blog> Blogs { get; set; } = new();
    }
}