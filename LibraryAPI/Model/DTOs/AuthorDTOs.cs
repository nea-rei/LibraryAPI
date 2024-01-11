using LibraryAPI.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Model.DTOs;

public class AuthorDTOs
{
    public class CreateAuthorDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [Range(10000, int.MaxValue, ErrorMessage = "Please fill in a number with a minimum of 5 digits (e.g. 12411)")]
        public int AuthorIdentity { get; set; }

    }
        public class UpdateAuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
    public class UpdateBookAuthorDTOs
    {
        public int Id { get; set; }
    }
    public class GetAuthorDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
