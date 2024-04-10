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

namespace CleanArchitecture.Application.Blog.Queries.GetBlogs
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, List<GetBlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IRedisCache _redisCache;

        public GetBlogsQueryHandler(IBlogRepository blogRepository, IRedisCache redisCache)
        {
            _blogRepository = blogRepository;
            _redisCache = redisCache;
        }

        public async Task<List<GetBlogResponse>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var cachedBlogData = await _redisCache.GetCacheData<List<BlogEntity>>("blogs");

            if (cachedBlogData != null)
            {
                return cachedBlogData.Adapt<List<GetBlogResponse>>();
            }
            var blogs = await _blogRepository.GetAllSync();
            await _redisCache.SetCacheData("blogs", blogs, TimeSpan.FromSeconds(30));
            return blogs.Adapt<List<GetBlogResponse>>();
        }
    }
}
