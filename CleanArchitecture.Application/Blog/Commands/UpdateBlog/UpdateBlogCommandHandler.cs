using CleanArchitecture.Application.Blog.DTOs;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, UpdateBlogResponse>
    {
        private readonly IBlogRepository _blogRepository;

        public UpdateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<UpdateBlogResponse> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var updatedBlog = request.Adapt<BlogEntity>();
            var blog = await _blogRepository.UpdateAsync(request.Id, updatedBlog);
            return blog.Adapt<UpdateBlogResponse>();
        }
    }
}
