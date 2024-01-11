using LibraryAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Model;
public class LibraryDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<LibraryUser> LibraryUsers { get; set; }
    public DbSet<Loan> LibraryLoans { get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LibraryUser>().HasIndex(u => u.LibraryCard).IsUnique();
        modelBuilder.Entity<Author>().HasIndex(u => u.AuthorIdentity).IsUnique();

        modelBuilder.Entity<Book>().HasData(new Book { Id = 1, Title = "Sea", ISBN = 54612312, ReleaseYear = 2010, Available = "False" });
        modelBuilder.Entity<Book>().HasData(new Book { Id = 2, Title = "Midnight", ISBN = 54612342, ReleaseYear = 2000, Available = "False" });
        modelBuilder.Entity<Book>().HasData(new Book { Id = 3, Title = "Happy", ISBN = 54122312, ReleaseYear = 1993, Available = "False" });

        modelBuilder.Entity<Author>().HasData(new Author { Id = 1, FirstName = "Anna", LastName = "Burns", AuthorIdentity = 456789 });
        modelBuilder.Entity<Author>().HasData(new Author { Id = 2, FirstName = "Bo", LastName = "Tag", AuthorIdentity = 456123});
        modelBuilder.Entity<Author>().HasData(new Author { Id = 3, FirstName = "Tom", LastName = "Pip", AuthorIdentity = 789464});

        modelBuilder.Entity<LibraryUser>().HasData(new LibraryUser { Id = 1, FirstName = "Sandra", LastName = "Woo", LibraryCard = 678345 });
        modelBuilder.Entity<LibraryUser>().HasData(new LibraryUser { Id = 2, FirstName = "Carl", LastName = "Loos", LibraryCard = 678346 });

        modelBuilder.Entity<Loan>().HasData(new Loan { Id = 1, LoanDate = DateTime.UtcNow.AddDays(-4), BookId = 1, LibraryUserId = 1 });
        modelBuilder.Entity<Loan>().HasData(new Loan { Id = 2, LoanDate = DateTime.UtcNow.AddDays(-2), BookId = 2, LibraryUserId = 1 });
        modelBuilder.Entity<Loan>().HasData(new Loan { Id = 3, LoanDate = DateTime.UtcNow.AddDays(-3), BookId = 3, LibraryUserId = 2 });

    }
}
