using BlogAPI.Features.Blogs.DTOs;
using MediatR;

namespace BlogAPI.Features.Blogs.Commands.UpdateBlog
{
    public sealed record UpdateBlogCommand(UpdateBlogDto Request) : IRequest;
}