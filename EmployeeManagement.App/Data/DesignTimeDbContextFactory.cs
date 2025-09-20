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

            // Look for the environment variable DB_CONNECTION_STRING (set in the pipeline)
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string not found. Ensure DB_CONNECTION_STRING is set in the pipeline."
                );
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new EmployeeDbContext(optionsBuilder.Options);
        }
    }
}
