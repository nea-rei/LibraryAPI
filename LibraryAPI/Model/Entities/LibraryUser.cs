using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Model.Entities;
[Table("LibraryUser", Schema = "Library")]
public class LibraryUser
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    [Range(10000, int.MaxValue, ErrorMessage = "Please fill in a number with a minimum of 5 digits (e.g. 12453)")]
    public int LibraryCard { get; set; }

    public List<Loan>? Loans { get; } = [];
}
