using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Features.Blogs.DTOs
{
    public class BlogItemDto
  {
      public int Id { get; set; }

      [Required(ErrorMessage = "Author is required")]
      public int? AuthorId { get; set; }

      [Required(ErrorMessage = "Title is required")]
      public required string Title { get; set; }
      
      [Required(ErrorMessage = "Short description is required")]
      public required string ShortDescription { get; set; }

      public string AuthorName { get; set; }
      public DateTime DatePublished { get; set; }
      public List<TagDto> Tags { get; set; }
  }
}