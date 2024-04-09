﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Queries.GetBlogs
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, List<BlogEntity>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogsQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<BlogEntity>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _blogRepository.GetAllSync();
            return blogs;
        }
    }
}