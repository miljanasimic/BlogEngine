using MediatR;

namespace BlogAPI.Features.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommand(int Id) : IRequest 
    {
      public int Id { get; set; } = Id;
    }
}