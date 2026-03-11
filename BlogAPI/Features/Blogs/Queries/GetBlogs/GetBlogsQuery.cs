using BlogAPI.Features.Blogs.DTOs;
using MediatR;

namespace BlogAPI.Features.Blogs.Queries.GetBlogs
{
  public class GetBlogsQuery(int? Tag = null, string? Language = null) : IRequest<IEnumerable<BlogItemDto>>
  {
      public int? Tag { get; } = Tag;
      public string? Language { get; } = Language;
  }
}