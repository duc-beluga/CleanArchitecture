using CleanArchitecture.Application.Blog.Commands.CreateBlog;
using CleanArchitecture.Application.Blog.Commands.UpdateBlog;
using CleanArchitecture.Application.Blog.Queries.GetBlogById;
using CleanArchitecture.Application.Blog.Queries.GetBlogs;
using CleanArchitecture.Application.Blog.Queries.GetBlogsRedis;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ISender _mediator;
        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _mediator.Send(new GetBlogsQuery());
            return Ok(blogs);
        }

        [HttpGet("Redis")]
        public async Task<IActionResult> GetBlogsRedis()
        {
            var blogs = await _mediator.Send(new GetBlogsRedisQuery());
            return Ok(blogs);
        }

        [HttpGet("{blogId}")]
        public async Task<IActionResult> GetBlogById(int blogId)
        {
            var blog = await _mediator.Send(new GetBlogByIdQuery() { Id = blogId });
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand)
        {
            var createdBlog = await _mediator.Send(createBlogCommand);
            return CreatedAtAction(nameof(GetBlogById), new { blogId = createdBlog.Id }, createdBlog);
        }

        [HttpPut("{blogId}")]
        public async Task<IActionResult> UpdateBlog(int blogId, UpdateBlogCommand updateBlogCommand)
        {
            if (blogId != updateBlogCommand.Id)
            {
                return BadRequest();
            }
            var updatedBlog = await _mediator.Send(updateBlogCommand);
            return Ok(updatedBlog);
        }
    }
}
