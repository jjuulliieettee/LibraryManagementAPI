using LibraryManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Order> Orders { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();
            
            modelBuilder.Entity<Order>()
                        .HasOne(e => e.Reader)
                        .WithMany(e => e.ReaderOrders)
                        .HasForeignKey(e => e.ReaderId);

            modelBuilder.Entity<Order>()
                        .HasOne(e => e.Librarian)
                        .WithMany(e => e.LibrarianOrders)
                        .HasForeignKey(e => e.LibrarianId);
        }
    }
}
