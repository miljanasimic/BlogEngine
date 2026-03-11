using BlogAPI.Context;
using BlogAPI.Exceptions;
using MediatR;

namespace BlogAPI.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly AppDbContext _context;

        public UpdateAuthorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException($"Author with Id {request.Id} not found");

            author.FirstName = request.FirstName;
            author.LastName = request.LastName;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}