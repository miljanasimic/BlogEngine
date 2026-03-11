using BlogAPI.Context;
using BlogAPI.Features.Authors.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors.Queries.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly AppDbContext _context;

        public GetAuthorsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Authors
                .AsNoTracking()
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                })
                .ToListAsync(cancellationToken);
        }
    }
}