using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Features.Blogs.DTOs
{
  public class BlogDetailsDto : BlogItemDto
  {
    [Required(ErrorMessage = "Content is required")]
    public required string Content { get; set; }

    [Required(ErrorMessage = "Language code is required")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Language code must be exactly 2 characters")]
    public required string Language { get; set; }
  }
}