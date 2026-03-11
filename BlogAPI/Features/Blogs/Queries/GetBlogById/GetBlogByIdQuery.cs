using BlogAPI.Features.Blogs.DTOs;
using MediatR;

namespace BlogAPI.Features.Blogs.Queries.GetBlogs
{
  public class GetBlogByIdQuery : IRequest<BlogDetailsDto>
  {
    public int Id { get; init; }

    public GetBlogByIdQuery(int id)
    {
      Id = id;
    }
  }
}