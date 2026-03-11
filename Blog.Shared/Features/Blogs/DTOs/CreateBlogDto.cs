using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Features.Blogs.DTOs
{
  public class CreateBlogDto
  {
    [Required]
    public required string Title  { get ; set; }

    [Required]
    public required string ShortDescription { get; set; }

    [Required]
    public required string Content { get; set; }

    [Required]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Language code must be exactly 2 characters")]
    public required string Language { get; set; }
    public List<string> Tags { get; set; } = new();

    [Required]
    public int AuthorId { get; set; }

    public DateTime? DatePublished { get; set; }
  }
}