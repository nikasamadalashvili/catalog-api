using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddCatalogContext(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<CatalogContext>(contextOptions =>
                {
                    contextOptions.UseSqlServer(
                        configuration.GetConnectionString("CatalogContext"),
                        optBuilder => { optBuilder.MigrationsAssembly(typeof(Startup).Assembly.FullName); });
                });

            return services;
        }
    }
}