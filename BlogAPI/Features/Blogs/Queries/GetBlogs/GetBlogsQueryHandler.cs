using BlogAPI.Context;
using BlogAPI.Features.Blogs.DTOs;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace BlogAPI.Features.Blogs.Queries.GetBlogs
{
  public sealed class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, IEnumerable<BlogItemDto>>
  {
      private readonly AppDbContext _context;

      public GetBlogsQueryHandler(AppDbContext context)
      {
          _context = context;
      }

      public async Task<IEnumerable<BlogItemDto>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
      {
            var query = _context.Blogs
              .AsNoTracking()
              .Include(b => b.Author)
              .Include(b => b.Tags)
              .AsQueryable();

            if (!string.IsNullOrEmpty(request.Language))
            {
                query = query.Where(b => b.Language == request.Language);
            }

            if (request.Tag.HasValue)
            {
                query = query.Where(b => b.Tags.Any(t => t.Id == request.Tag));
            }

            return await query
                .Select(b => new BlogItemDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    ShortDescription = b.ShortDescription,
                    AuthorId = b.AuthorId,
                    AuthorName = $"{b.Author.FirstName} {b.Author.LastName}",
                    DatePublished = b.DatePublished,
                    Tags = b.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList()
                })
                .ToListAsync(cancellationToken);
      }
  }
}