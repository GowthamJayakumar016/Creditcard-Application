using Creditcard.Application.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Creditcard.Application.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Application> Applications => Set<Application>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Application>()
            .HasIndex(a => a.ApplicationNumber)
            .IsUnique();

        modelBuilder.Entity<Application>()
            .HasOne(a => a.User)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Application>()
            .HasCheckConstraint("CK_Application_CreditScore", "CreditScore BETWEEN 600 AND 900");

        modelBuilder.Entity<Application>()
            .HasCheckConstraint("CK_Application_Status", "Status IN ('Pending','Approved','Rejected','ManualReview')");

        base.OnModelCreating(modelBuilder);
    }
}
