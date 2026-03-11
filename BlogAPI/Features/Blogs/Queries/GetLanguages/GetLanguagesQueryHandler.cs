using BlogAPI.Context;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Blogs.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Blogs.Queries.GetLanguages
{
    public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, IEnumerable<string>>
    {
        private readonly AppDbContext _context;

        public GetLanguagesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Blogs
                .AsNoTracking()
                .Select(b => b.Language)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
    }
}