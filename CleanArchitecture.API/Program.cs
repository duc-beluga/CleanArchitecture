using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Infrastructure.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

if (builder.Environment.IsProduction())
{
    builder.Services.AddScoped<IBlogRepository, BlogRepository>();

    var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");

    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback));

    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), new DefaultAzureCredential());

    builder.Services.AddDbContext<BlogDbContext>(options =>
        options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString() ??
            throw new InvalidOperationException("'ProdConnection' not found")
        )
    );

    builder.Services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(client.GetSecret("RedisUrl").Value.Value.ToString() ??
        throw new InvalidOperationException("'RedisUrl' not found")));
    builder.Services.AddScoped<IRedisCache, RedisCache>();
}
else if (builder.Environment.IsDevelopment())
{
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(builder.Configuration.GetSection("Redis:RedisUrl").Value!.ToString() ??
       throw new InvalidOperationException("'RedisUrl' not found")));
    builder.Services.AddScoped<IRedisCache, RedisCache>();
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
