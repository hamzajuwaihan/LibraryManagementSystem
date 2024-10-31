using ManagementLibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Infrastructure.DB;

public class DbAppContext(DbContextOptions<DbAppContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Library> Libraries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne<Member>()
            .WithMany(m => m.Books)
            .HasForeignKey(b => b.BorrowedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.LibraryAssociated)
            .WithMany(l => l.Books)
            .HasForeignKey(b => b.LibraryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Library>()
            .HasIndex(l => l.Name)
            .IsUnique();

        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique();

        modelBuilder.Entity<Library>()
            .HasMany(l => l.Members)
            .WithMany(m => m.Libraries)
            .UsingEntity(j => j.ToTable("LibraryMembers"));
    }
}
