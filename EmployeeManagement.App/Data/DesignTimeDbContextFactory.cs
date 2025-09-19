using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace EmployeeManagement.App.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
    {
        public EmployeeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();

            // Read connection string from environment variable set in GitHub Actions pipeline
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string not found. Ensure DB_CONNECTION_STRING is set in the pipeline.");
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new EmployeeDbContext(optionsBuilder.Options);
        }
    }
}
