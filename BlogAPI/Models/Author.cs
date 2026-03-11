using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
  public class Author
  {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "First name is required")]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required")]
    public required string LastName { get; set; }
  }
}