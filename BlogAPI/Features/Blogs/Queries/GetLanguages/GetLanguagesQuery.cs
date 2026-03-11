using MediatR;

namespace BlogAPI.Features.Blogs.Queries.GetLanguages
{
  public class GetLanguagesQuery : IRequest<IEnumerable<string>> {}
}