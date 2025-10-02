using Database.Core;
using Database.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class StartUp
    {
        public static IServiceCollection AddGenericRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }

        /// <summary>
        /// Registers a DbContext with MySQL database provider.
        /// </summary>
        public static IServiceCollection AddDBContextMySQL<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            return services;
        }

        /// <summary>
        /// Registers a DbContext with PostgreSQL database provider.
        /// </summary>
        public static IServiceCollection AddDBContextPostgreSQL<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }

        /// <summary>
        /// Registers a DbContext with MS SQL Server database provider.
        /// </summary>
        public static IServiceCollection AddDBContextSqlServer<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
