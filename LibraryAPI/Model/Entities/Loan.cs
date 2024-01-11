using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model.Entities;

[Table("Loan", Schema = "Library")]
public class Loan
{
    [Key]
    public int Id { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    [Range(1,5, ErrorMessage = "Please rate the book with a number between 1 and 5")]
    public double Rating { get; set; }
    public bool Available { get; set; }
    [Required]
    public int BookId { get; set; }
    [Required]
    public int LibraryUserId { get; set; }
}
