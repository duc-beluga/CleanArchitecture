using CleanArchitecture.Application.Blog.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Queries.GetBlogsRedis
{
    public class GetBlogsRedisQuery : IRequest<List<GetBlogResponse>>
    {
    }
}
