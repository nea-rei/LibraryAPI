using LibraryAPI.Model.Entities;
using System.ComponentModel.DataAnnotations;
using static LibraryAPI.Model.DTOs.AuthorDTOs;

namespace LibraryAPI.Model.DTOs;

public class BookDTOs
{
    public class BookDTO
    {
        public string Title { get; set; } = null!;
        public int ISBN { get; set; }
        [Range(1400, 2100, ErrorMessage = "Please fill in the year the book was released")]
        public int ReleaseYear { get; set; }
        public List<Author>? Authors { get; set; } = [];
    }
    public class UpdateBookAuthorDTO
    {
        public List<UpdateBookAuthorDTOs>? Authors { get; set; } = [];
    }

    public class GetBooksDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<GetAuthorDTO>? Authors { get; set; } = [];
    }

    public class GetBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ISBN { get; set; }
        public int ReleaseYear { get; set; }
        public double? AverageRating { get; set; }
        public string Available { get; set; } = null!;
        public List<GetAuthorDTO>? Authors { get; set; } = [];
    }
}
