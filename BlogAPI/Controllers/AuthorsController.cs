using BlogAPI.Features.Authors.Commands.CreateAuthor;
using BlogAPI.Features.Authors.Commands.DeleteAuthor;
using BlogAPI.Features.Authors.Commands.UpdateAuthor;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Authors.Queries.GetAuthorById;
using BlogAPI.Features.Authors.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthorsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateAuthorCommand command)
        {
            var id = await _sender.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _sender.Send(new GetAuthorsQuery());
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            var author = await _sender.Send(new GetAuthorByIdQuery { Id = id });
            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorCommand command)
        {
            if (id != command.Id) return BadRequest();
            await _sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }
    }
}