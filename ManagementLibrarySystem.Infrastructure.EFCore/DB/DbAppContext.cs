using ManagementLibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.EFCore.DB;

public class DbAppContext(DbContextOptions<DbAppContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Library> Libraries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Book entity
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.HasOne(b => b.Member)
                  .WithMany(m => m.Books)
                  .HasForeignKey(b => b.BorrowedBy)
                  .OnDelete(DeleteBehavior.Restrict);

            // Define foreign key relationship between Book and Library
            entity.HasOne<Library>()
                  .WithMany(l => l.Books)
                  .HasForeignKey(b => b.LibraryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.HasMany(m => m.Books)
                  .WithOne(b => b.Member)
                  .HasForeignKey(b => b.BorrowedBy)
                  .OnDelete(DeleteBehavior.SetNull);

            // Define foreign key relationship between Member and Library
            entity.HasOne<Library>()
                  .WithMany(l => l.Members)
                  .HasForeignKey(m => m.LibraryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.HasMany(l => l.Books)
                  .WithOne()
                  .HasForeignKey(b => b.LibraryId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(l => l.Members)
                  .WithOne()
                  .HasForeignKey(m => m.LibraryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }

}