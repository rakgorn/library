using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class LibraryContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=LibraryDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}