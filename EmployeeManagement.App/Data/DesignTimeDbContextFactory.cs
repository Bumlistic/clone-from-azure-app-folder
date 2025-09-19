using EmployeeManagement.App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace EmployeeManagement.App.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDBContext>
    {
        public EmployeeDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDBContext>();
            
            // Use your actual connection string from environment or secrets
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            
            optionsBuilder.UseSqlServer(connectionString);

            return new EmployeeDBContext(optionsBuilder.Options);
        }
    }
}
