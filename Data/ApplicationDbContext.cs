using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stomatologia.Models;

namespace Stomatologia.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Dentist> Dentists { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Dentist)
            .WithMany()
            .HasForeignKey(v => v.DentistId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Visit>()
            .HasOne(v => v.Client)
            .WithMany()
            .HasForeignKey(v => v.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
