using BlogAPI.Context;
using BlogAPI.Exceptions;
using BlogAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>
    {
        private readonly AppDbContext _context;

        public UpdateBlogCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var updateBlogDto = request.Request;
            var blog = await _context.Blogs
                .Include(b => b.Tags)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == updateBlogDto.Id, cancellationToken)
                ?? throw new NotFoundException($"Blog with Id {updateBlogDto.Id} not found");

            var author = await _context.Authors.FindAsync(updateBlogDto.AuthorId, cancellationToken);
            if (author is null)
                throw new NotFoundException($"Author with ID {updateBlogDto.AuthorId} not found");

            blog.Title = updateBlogDto.Title;
            blog.ShortDescription = updateBlogDto.ShortDescription;
            blog.Content = updateBlogDto.Content;
            blog.Language = updateBlogDto.Language;
            blog.Author = author;
            blog.DatePublished = updateBlogDto.DatePublished ?? blog.DatePublished;

            // Handle tags
            var existingTags = await _context.Tags
                .Where(t => updateBlogDto.Tags.Contains(t.Name))
                .ToListAsync(cancellationToken);

            var newTags = updateBlogDto.Tags
                .Where(name => existingTags.All(t => t.Name != name))
                .Select(name => new Tag { Name = name })
                .ToList();

            if (newTags.Any())
                _context.Tags.AddRange(newTags);

            blog.Tags = existingTags.Concat(newTags).ToList();

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}