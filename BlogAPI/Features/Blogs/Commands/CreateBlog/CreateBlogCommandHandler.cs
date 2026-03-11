using BlogAPI.Context;
using BlogAPI.Exceptions;
using BlogAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateBlogCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var blogDto = request.Request;
            var author = await _context.Authors.FindAsync(blogDto.AuthorId, cancellationToken);

            if (author is null)
                throw new NotFoundException($"Author with ID {blogDto.AuthorId} not found");

            var existingTags = await _context.Tags
                .Where(t => blogDto.Tags.Contains(t.Name))
                .ToListAsync(cancellationToken);

            var newTags = blogDto.Tags
                .Where(name => existingTags.All(t => t.Name != name))
                .Select(name => new Tag { Name = name })
                .ToList();

            var allTags = existingTags.Concat(newTags).ToList();

             if (newTags.Any())
            {
                _context.Tags.AddRange(newTags);
            }
            var blog = new Blog
            {
                Title = blogDto.Title,
                ShortDescription = blogDto.ShortDescription,
                Content = blogDto.Content,
                Language = blogDto.Language,
                DatePublished = blogDto.DatePublished ?? DateTime.UtcNow,
                Author = author,
                Tags = allTags
            };

            _context.Blogs.Add(blog);

            await _context.SaveChangesAsync(cancellationToken);

            return blog.Id;
        }
    }
}