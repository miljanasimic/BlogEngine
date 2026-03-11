using BlogAPI.Context;
using BlogAPI.Exceptions;
using MediatR;

namespace BlogAPI.Features.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand>
    {
        private readonly AppDbContext _context;

        public DeleteBlogCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs
                .FindAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException($"Blog with Id {request.Id} not found");

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}