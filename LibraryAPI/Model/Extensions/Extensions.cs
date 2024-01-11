using LibraryAPI.Model.Entities;
using static LibraryAPI.Model.DTOs.AuthorDTOs;
using static LibraryAPI.Model.DTOs.BookDTOs;

namespace LibraryAPI.Model.Extensions
{
    public static class Extensions
    {
        public static GetBooksDTO BooksToDTO(this Book book)
        {
            List<GetAuthorDTO>? getAuthorDTOs = book.Authors?
            .Select(a => new GetAuthorDTO()
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
            })
            .ToList();

            return new GetBooksDTO() {Id = book.Id, Title = book.Title, Authors = getAuthorDTOs };
        }
    }
}

