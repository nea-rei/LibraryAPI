using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model.Entities;
[Table("Book", Schema = "Library")]
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int ISBN { get; set; }
    [Range(1400, 2100, ErrorMessage = "Please fill in the year the book was released")]
    public int ReleaseYear { get; set; }
    public double? Rating { get; set; }
    public string Available { get; set; } = null!;

    public List<Author>? Authors { get; set; } = [];
}