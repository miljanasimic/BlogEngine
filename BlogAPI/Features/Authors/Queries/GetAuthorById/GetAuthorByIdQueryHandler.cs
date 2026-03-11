using BlogAPI.Context;
using BlogAPI.Exceptions;
using BlogAPI.Features.Authors.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly AppDbContext _context;

        public GetAuthorByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors
                .FindAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException($"Author with Id {request.Id} not found");

            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }
    }
}