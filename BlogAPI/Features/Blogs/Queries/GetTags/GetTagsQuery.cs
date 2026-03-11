using BlogAPI.Features.Blogs.DTOs;
using MediatR;

namespace BlogAPI.Features.Blogs.Queries.GetTags
{
  public class GetTagsQuery : IRequest<IEnumerable<TagDto>> {}
}