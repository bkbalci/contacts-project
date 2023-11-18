using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Contexts;

public class ContactDbContext : DbContext
{
    
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