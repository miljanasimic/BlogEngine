using BlogAPI.Features.Authors.DTOs;
using MediatR;

namespace BlogAPI.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>> { }
}