using BlogAPI.Context;
using BlogAPI.Exceptions;
using MediatR;

namespace BlogAPI.Features.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly AppDbContext _context;

        public DeleteAuthorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException($"Author with Id {request.Id} not found");

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}