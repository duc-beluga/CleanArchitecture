﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _blogDbContext.Blogs.AddAsync(blog);
            await _blogDbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _blogDbContext.Blogs.Where(blog => blog.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<Blog>> GetAllSync()
        {
            return await _blogDbContext.Blogs.ToListAsync();
        }

        public async Task<Blog> GetByIdSync(int id)
        {
            return await _blogDbContext.Blogs.AsNoTracking().FirstOrDefaultAsync(blog => blog.Id == id);
        }

        public async Task<int> UpdateAsync(int id, Blog blog)
        {
            return await _blogDbContext.Blogs.Where(b => b.Id == id).ExecuteUpdateAsync(setters => setters
            .SetProperty(b => b.Id, blog.Id)
            .SetProperty(b => b.Name, blog.Name)
            .SetProperty(b => b.Description, blog.Description)
            .SetProperty(b => b.Author, blog.Author)
            .SetProperty(b => b.ImageUrl, blog.ImageUrl));
        }
    }
}
