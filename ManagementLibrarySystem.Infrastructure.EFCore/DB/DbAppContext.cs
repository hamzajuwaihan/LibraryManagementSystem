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
            
            modelBuilder.Entity<Book>(entity =>
            {
                  entity.HasKey(b => b.Id);
                  entity.HasMany(b => b.Libraries) 
                  .WithMany(l => l.Books);

                  entity.HasOne(b => b.Member)
                  .WithMany(m => m.Books)
                  .HasForeignKey(b => b.BorrowedBy)
                  .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<Member>(entity =>
            {
                  entity.HasKey(m => m.Id);
                  entity.HasMany(m => m.Libraries) 
                  .WithMany(l => l.Members);
            });


            modelBuilder.Entity<Library>(entity =>
            {
                  entity.HasKey(l => l.Id);
            });
      }

}