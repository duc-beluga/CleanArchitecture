using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }
        public async Task<BlogEntity> CreateAsync(BlogEntity blog)
        {
            await _blogDbContext.Blogs.AddAsync(blog);
            await _blogDbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _blogDbContext.Blogs.Where(blog => blog.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<BlogEntity>> GetAllSync()
        {
            return await _blogDbContext.Blogs.ToListAsync();
        }

        public async Task<BlogEntity?> GetByIdSync(int id)
        {
            var article = await _blogDbContext.Blogs.FindAsync(id);
            return article;
        }

        public async Task<BlogEntity?> UpdateAsync(int id, BlogEntity blog)
        {
            var selectedBlog = await GetByIdSync(id);
            if (selectedBlog is null)
                return null;
            selectedBlog.Author = blog.Author;
            selectedBlog.Description = blog.Description;
            selectedBlog.Name = blog.Name;
            selectedBlog.ImageUrl = blog.ImageUrl;

            await _blogDbContext.SaveChangesAsync();

            return selectedBlog;
        }
    }
}
