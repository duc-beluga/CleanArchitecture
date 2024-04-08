using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.GetBlogs
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, List<Blog>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogsQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<Blog>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _blogRepository.GetAllSync();
            return blogs;
        }
    }
}
