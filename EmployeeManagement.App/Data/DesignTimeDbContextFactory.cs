using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EmployeeManagement.App.Data; // Make sure this points to your EmployeeDbContext namespace

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
{
    public EmployeeDbContext CreateDbContext(string[] args)
    {
        // Read the connection string from environment variable first
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "No connection string found. Set DB_CONNECTION_STRING environment variable.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new EmployeeDbContext(optionsBuilder.Options);
    }
}
