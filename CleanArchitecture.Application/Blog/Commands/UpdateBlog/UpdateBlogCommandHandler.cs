using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, BlogEntity>
    {
        private readonly IBlogRepository _blogRepository;

        public UpdateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogEntity> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var updatedBlog = new BlogEntity
            {
                Id = request.Id,
                Author = request.Author,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Name = request.Name
            };
            var blog = await _blogRepository.UpdateAsync(request.Id, updatedBlog);
            return blog;
        }
    }
}
