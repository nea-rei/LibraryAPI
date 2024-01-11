using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model.Entities;
[Table("Author", Schema = "Library")]
public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    [Range(10000, int.MaxValue, ErrorMessage = "Please fill in a number with a minimum of 5 digits (e.g. 12411)")]
    public int AuthorIdentity { get; set; }

    public List<Book> Books { get; } = [];
}
