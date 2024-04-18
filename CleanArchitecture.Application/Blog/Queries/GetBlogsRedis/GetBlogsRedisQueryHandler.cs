using CleanArchitecture.Application.Blog.DTOs;
using CleanArchitecture.Application.Blog.Queries.GetBlogs;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Queries.GetBlogsRedis
{
    public class GetBlogsRedisQueryHandler : IRequestHandler<GetBlogsQuery, List<GetBlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IRedisCache _redisCache;

        public GetBlogsRedisQueryHandler(IBlogRepository blogRepository, IRedisCache redisCache)
        {
            _blogRepository = blogRepository;
            _redisCache = redisCache;
        }

        public async Task<List<GetBlogResponse>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var cachedBlogData = await _redisCache.GetCacheData<List<BlogEntity>>("blogs");
                
            return cachedBlogData.Adapt<List<GetBlogResponse>>();

        }
    }
}
