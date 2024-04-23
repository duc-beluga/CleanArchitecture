using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Data
{
    public class BlogDbContext : IdentityDbContext<UserEntity>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<BlogEntity> Blogs { get; set; }
    }
}
