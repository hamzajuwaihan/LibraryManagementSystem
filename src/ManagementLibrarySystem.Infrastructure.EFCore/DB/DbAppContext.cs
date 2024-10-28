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
    modelBuilder.Entity<Book>()
        .HasOne<Member>()
        .WithMany(m => m.Books)
        .HasForeignKey(b => b.BorrowedBy) 
        .OnDelete(DeleteBehavior.SetNull)
        .IsRequired(false);

  }
}
