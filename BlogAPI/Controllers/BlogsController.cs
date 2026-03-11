using BlogAPI.Features.Blogs.Commands.CreateBlog;
using BlogAPI.Features.Blogs.Commands.DeleteBlog;
using BlogAPI.Features.Blogs.Commands.UpdateBlog;
using BlogAPI.Features.Blogs.DTOs;
using BlogAPI.Features.Blogs.Queries.GetBlogs;
using BlogAPI.Features.Blogs.Queries.GetLanguages;
using BlogAPI.Features.Blogs.Queries.GetTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BlogsController : ControllerBase
  {
      private readonly ISender _sender;

      public BlogsController(ISender sender)
      {
          _sender = sender;
      }

      [HttpPost]
      public async Task<ActionResult<int>> CreateBlog([FromBody] CreateBlogDto blogDto)
      {
          var blogId = await _sender.Send(new CreateBlogCommand(blogDto));
          return Ok(blogId);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateBlog(int id, [FromBody] UpdateBlogDto blogDto)
      {
          if (id != blogDto.Id) return BadRequest();
          await _sender.Send(new UpdateBlogCommand(blogDto));
          return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteBlog(int id)
      {
          await _sender.Send(new DeleteBlogCommand(id));
          return NoContent();
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<BlogDetailsDto>> GetBlogById(int id)
      {
          var blog = await _sender.Send(new GetBlogByIdQuery(id));
          if (blog == null) return NotFound();
          return Ok(blog);
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<BlogItemDto>>> GetBlogs([FromQuery] int? tag, [FromQuery] string? language)
      {
          var blogs = await _sender.Send(new GetBlogsQuery(tag, language));
          return Ok(blogs);
      }

      [HttpGet("tags")]
      public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
      {
          var tags = await _sender.Send(new GetTagsQuery());
          return Ok(tags);
      }

      [HttpGet("languages")]
      public async Task<ActionResult<IEnumerable<string>>> GetLanguages()
      {
          var languages = await _sender.Send(new GetLanguagesQuery());
          return Ok(languages);
      }
  }
}