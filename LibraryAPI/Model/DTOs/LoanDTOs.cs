using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Model.DTOs;

public class LoanDTOs
{
    public record BorrowBookDTO(int LibraryUserId, int BookId);
    public class ReturnBookDTO
    {
        [Range(1, 5, ErrorMessage = "Please rate the book with a number between 1 and 5")]
        public double Rating { get; set; }
    }
}
