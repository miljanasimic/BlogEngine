using BlogAPI.Context;
using BlogAPI.Models;
using MediatR;

namespace BlogAPI.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateAuthorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync(cancellationToken);

            return author.Id;
        }
    }
}