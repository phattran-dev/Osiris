using Database.Constants;
using Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Osiris.IdentityService.Infrastructure
{
    public class OsirisIdentityDbContextFactory : IDesignTimeDbContextFactory<OsirisIdentityDbContext>
    {
        public OsirisIdentityDbContext CreateDbContext(string[] args)
        {
            // Load database configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Get the connection string for the Identity database
            var configurationPath = $"{DBEngineConstants.RootConnectionString}:{SystemNames.Identity}{DBEngineConstants.DbConnectionStringPrefix}";
            var connectionString = (args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0]))
                ? args[0] : configuration[configurationPath];

            var optionsBuilder = new DbContextOptionsBuilder<OsirisIdentityDbContext>();
            // Use MySQL as the database provider
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            // Use PostgreSQL as the database provider
            //optionsBuilder.UseNpgsql(connectionString);

            // Use SQL Server as the database provider
            optionsBuilder.UseSqlServer(connectionString);

            return new OsirisIdentityDbContext(optionsBuilder.Options);
        }
    }
}
