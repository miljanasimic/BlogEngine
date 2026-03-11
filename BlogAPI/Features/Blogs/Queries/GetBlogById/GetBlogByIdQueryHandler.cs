using BlogAPI.Context;
using BlogAPI.Exceptions;
using BlogAPI.Features.Blogs.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Blogs.Queries.GetBlogs
{

  public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, BlogDetailsDto>
  {
    private readonly AppDbContext _context;

      public GetBlogByIdQueryHandler(AppDbContext context)
      {
        _context = context;
      }

    public async Task<BlogDetailsDto> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
      var blog = await _context.Blogs
          .AsNoTracking()
          .Include(b => b.Author)
          .Include(b => b.Tags)
          .FirstOrDefaultAsync(
              b => b.Id == request.Id, 
              cancellationToken);

      if (blog is null)
        throw new NotFoundException($"Blog with ID {request.Id} not found.");
      

      return new BlogDetailsDto
      {
          Id = blog.Id,
          Title = blog.Title,
          ShortDescription = blog.ShortDescription,
          Content = blog.Content,
          Language = blog.Language,
          AuthorId = blog.AuthorId,
          AuthorName = $"{blog.Author.FirstName} {blog.Author.LastName}",
          DatePublished = blog.DatePublished,
          Tags = blog.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name }).ToList()
      };
    }
  }
}