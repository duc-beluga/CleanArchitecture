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

namespace CleanArchitecture.Application.Blog.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogResponse>
    {
        private readonly IBlogRepository _blogRepository;

        public CreateBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<CreateBlogResponse> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var newBlog = request.Adapt<BlogEntity>();
            var blog = await _blogRepository.CreateAsync(newBlog);
            return blog.Adapt<CreateBlogResponse>();
        }
    }
}
