using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BlogAPI.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<int>
    {

        [Required]
        public  required string FirstName { get; set; }
        [Required]
        public  required string LastName { get; set; }
    }
}