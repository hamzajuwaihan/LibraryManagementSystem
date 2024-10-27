using ManagementLibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Infrastructure.EFCore.DB;

public class DbAppContext(DbContextOptions<DbAppContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Library> Libraries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book entity configuration
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);

            // Explicitly configure ID as a UUID type with default generation in PostgreSQL
            entity.Property(b => b.Id)
                  .HasColumnType("uuid")
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.HasMany(b => b.Libraries)
                  .WithMany(l => l.Books);

            entity.HasOne(b => b.Member)
                  .WithMany(m => m.Books)
                  .HasForeignKey(b => b.BorrowedBy)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Member entity configuration
        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.Id)
                  .HasColumnType("uuid")
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.HasMany(m => m.Libraries)
                  .WithMany(l => l.Members);
        });

        // Library entity configuration
        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.Id)
                  .HasColumnType("uuid")
                  .HasDefaultValueSql("uuid_generate_v4()");
        });
    }

}