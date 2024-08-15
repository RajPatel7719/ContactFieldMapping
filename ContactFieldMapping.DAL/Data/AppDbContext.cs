using ContactFieldMapping.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactFieldMapping.DAL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().ToTable("Contacts");
        modelBuilder.Entity<Contact>().HasKey(c => c.ContactKey);
    }
}
