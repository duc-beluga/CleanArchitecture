using CleanArchitecture.Application.Blog.DTOs;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Blog.Queries.GetBlogById
{
    public class GetBlogByIdQuery : IRequest<GetBlogResponse>
    {
        public int Id { get; set; }
    }
}
