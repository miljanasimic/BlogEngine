namespace BlogAPI.Features.Blogs.DTOs
{
  public class UpdateBlogDto : CreateBlogDto
  {
    public required int Id { get; set; }
  }
}