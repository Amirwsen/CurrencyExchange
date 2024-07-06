using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>()
            .HasKey(currency => currency.Id);
        modelBuilder.Entity<Currency>()
            .HasQueryFilter(currency => currency.DeletedAt == null)
            .HasOne(currency => currency.Creator)
            .WithMany()
            .HasForeignKey(currency => currency.CreatorId)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasKey(user => user.Id);
        modelBuilder.Entity<User>()
            .HasQueryFilter(user => user.DeletedAt == null)
            .Property(user => user.Username).IsRequired();
        modelBuilder.Entity<User>().Property(user => user.Password).IsRequired();
        modelBuilder.Entity<User>().Property(user => user.Email).IsRequired();
        modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique();
    }
}