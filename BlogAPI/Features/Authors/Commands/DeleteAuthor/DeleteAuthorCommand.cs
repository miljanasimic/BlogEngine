using MediatR;

namespace BlogAPI.Features.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand(int id) : IRequest
    {
      public int Id { get; set; } = id;
    }
}