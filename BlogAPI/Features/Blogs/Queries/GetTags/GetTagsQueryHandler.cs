using BlogAPI.Context;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Blogs.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Blogs.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<TagDto>>
    {
        private readonly AppDbContext _context;

        public GetTagsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tags
                .AsNoTracking()
                .Select(t => new TagDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}