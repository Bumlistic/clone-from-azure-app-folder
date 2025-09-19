using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EmployeeManagement.App.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Try to get connection string from environment variable first
            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback: read from appsettings.json if environment variable not set
                var basePath = Directory.GetCurrentDirectory();
                var config = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                connectionString = config.GetConnectionString("DefaultConnection");
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found. Set 'ConnectionStrings__DefaultConnection' environment variable or provide it in appsettings.json.");
            }

            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(); // transient fault resiliency
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
