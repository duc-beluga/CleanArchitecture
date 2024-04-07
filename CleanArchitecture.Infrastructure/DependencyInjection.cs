using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BlogDbConnectionString") ??
                throw new InvalidOperationException("'BlogDbConnectionString' not found"))
            );

            services.AddScoped<IBlogRepository, BlogRepository>();
            return services;
        }
    }
}
