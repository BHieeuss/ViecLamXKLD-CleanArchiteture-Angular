using ViecLam.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Infrastructure.Repositories;

namespace ViecLam.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Adds the AppDbContext with SQL Server configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // Registers the Blog
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;
        }
    }
}
