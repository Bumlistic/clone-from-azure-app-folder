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

        // Read from environment variable
        var connString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
        if (string.IsNullOrEmpty(connString))
            throw new InvalidOperationException("Connection string not found. Make sure ConnectionStrings__DefaultConnection is set.");

        optionsBuilder.UseSqlServer(connString);
        return new ApplicationDbContext(optionsBuilder.Options);
    }
  }
}
