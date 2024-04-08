using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Queries.GetBlogById
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, BlogEntity>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogByIdQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogEntity> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdSync(request.Id);
            return blog;

        }
    }
}
