using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EmployeeManagement.App.Data; // ✅ adjust if EmployeeDBContext is in another namespace
using System;

namespace EmployeeManagement.App.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDBContext>
    {
        public EmployeeDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDBContext>();

            // Get connection string from environment variable
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found. Make sure it is set in GitHub Actions.");
            }

            optionsBuilder.UseSqlServer(connectionString);

            return new EmployeeDBContext(optionsBuilder.Options);
        }
    }
}
