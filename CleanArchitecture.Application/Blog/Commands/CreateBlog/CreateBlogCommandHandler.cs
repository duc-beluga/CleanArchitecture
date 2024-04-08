using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BlogEntity>
    {
        private readonly IBlogRepository _blogRepository;

        public CreateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogEntity> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var newBlog = new BlogEntity
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Author = request.Author,
                ImageUrl = request.ImageUrl
            };
            var blog = await _blogRepository.CreateAsync(newBlog);
            return blog;
        }
    }
}
