
using BlogAPI.Features.Blogs.DTOs;
using MediatR;

namespace BlogAPI.Features.Blogs.Commands.CreateBlog
{
  public sealed record CreateBlogCommand(CreateBlogDto Request) : IRequest<int>;
}