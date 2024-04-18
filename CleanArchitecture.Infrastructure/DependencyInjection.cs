using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
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
            services.AddScoped<IBlogRepository, BlogRepository>();

            var keyVaultUrl = configuration.GetSection("KeyVault:KeyVaultURL");
            var secretClient = new SecretClient(new Uri(keyVaultUrl.Value!.ToString()), new DefaultAzureCredential());
            
            var RedisConnectionString = secretClient.GetSecret("RedisUrl").Value.Value.ToString();

            var SQLConnectionString = "";
            if (configuration["ASPNETCORE_ENVIRONMENT"] == "Production")
            {
                SQLConnectionString = secretClient.GetSecret("AzureSQL-ConnectionString-Production").Value.Value.ToString();
            } 
            else if (configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
            {
                SQLConnectionString = configuration.GetConnectionString("BlogDbDevelopmentConnectionString");

                //SQLConnectionString = secretClient.GetSecret("AzureSQL-ConnectionString-Development").Value.Value.ToString();
            }    

            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(SQLConnectionString 
                                                ?? throw new InvalidOperationException("'BlogDbDevelopmentConnectionString' not found"))
            );    
            
            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(RedisConnectionString
                                                ?? throw new InvalidOperationException("'RedisUrl' not found")));

            services.AddScoped<IRedisCache, RedisCache>();

            return services;
        }
    }
}
