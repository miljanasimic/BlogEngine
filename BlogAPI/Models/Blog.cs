using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
  public class Blog
  {
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public required string Title { get; set; }
    
    [Required(ErrorMessage = "Short description is required")]
    public required string ShortDescription { get; set; }
    
    [Required(ErrorMessage = "Content is required")]
    public required string Content { get; set; }
    
    [Required(ErrorMessage = "Language code is required")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Language code must be exactly 2 characters")]
    public required string Language { get; set; }
    
    [Required(ErrorMessage = "Tags are required")]
    public required List<Tag> Tags { get; set; }
    
    [Required(ErrorMessage = "Date published is required")]
    public DateTime DatePublished { get; set; }
    
    [Required(ErrorMessage = "Author ID is required")]
    public int AuthorId { get; set; }
    
    [Required(ErrorMessage = "Author is required")]
    public required Author Author { get; set; }
  }
}