using ContactFieldMapping.DAL.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ContactFieldMapping.DAL;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ContactMapping;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
