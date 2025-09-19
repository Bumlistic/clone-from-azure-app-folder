using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EmployeeManagement.App.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
    {
        public EmployeeDbContext CreateDbContext(string[] args)
        {
            // Load configuration from appsettings.json and environment variables
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Try multiple sources for connection string
            var connectionString =
                Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING") ??
                configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "No connection string found. " +
                    "Set DB_CONNECTION_STRING, SQL_CONNECTION_STRING, or ConnectionStrings__DefaultConnection.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EmployeeDbContext(optionsBuilder.Options);
        }
    }
}
