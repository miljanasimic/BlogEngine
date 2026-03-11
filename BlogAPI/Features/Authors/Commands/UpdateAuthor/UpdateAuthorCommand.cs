using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BlogAPI.Features.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }      
        [Required]
        public required string LastName { get; set; } = null!;
    }
}