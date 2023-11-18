using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Contexts;

public class ContactDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserContactInfo> UserContactInfos { get; set; }
    
    public ContactDbContext(DbContextOptions<ContactDbContext> options)
        : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

}