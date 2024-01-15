using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Model.Entities;
using static LibraryAPI.Model.DTOs.BookDTOs;
using static LibraryAPI.Model.DTOs.AuthorDTOs;
using LibraryAPI.Model.Extensions;
using LibraryAPI.Model;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBooksDTO>>> GetBooks()
        {
            var books = await _context.Books.OrderBy(a => a.Title)
                .AsNoTracking()
                .Include(b => b.Authors)
                .Select(b => b.BooksToDTO())
                .ToListAsync();

            return books;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookDTO>> GetBook(int id)
        {
            double averagerating;
            string available;

            Book? book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            if (BookLoanExists(id))
            {
                averagerating = _context.LibraryLoans
                  .Where(x => x.BookId == id)
                  .Average(x => x.Rating);

                available = _context.LibraryLoans
                .Where(x => x.BookId == id)
                .OrderBy(x => x.Id)
                .Select(x => x.Available).Last().ToString();
            }
            else
            {
                averagerating = 0;
                available = "true";
            }

            List<GetAuthorDTO>? getAuthorDTOs = book.Authors?
                .Select(a => new GetAuthorDTO()
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                })
                .ToList();

                return new GetBookDTO()
                {
                    Id = book.Id, Title = book.Title, ISBN = book.ISBN, ReleaseYear = book.ReleaseYear,
                    AverageRating = averagerating, Available = available, Authors = getAuthorDTOs
                };
        }
        [HttpPut("AddAuthorToBook/{id}")]
        public async Task<IActionResult> PutBookAuthor(int id, UpdateBookAuthorDTO addAuthorDTO)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .SingleAsync(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            var addAuthorId = addAuthorDTO.Authors?.Select(s => s.Id).ToList();

            if (addAuthorId is not null)
            {
                var authors = _context.Authors.Where(a => addAuthorId.Contains(a.Id)).ToList();
                if (authors.Count == 0)
                {
                    return BadRequest("Error - author does not exist");
                }
                else
                book.Authors?.AddRange(authors);
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO updatebookDTO)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .SingleAsync(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            book.Title = updatebookDTO.Title;
            book.ISBN = updatebookDTO.ISBN;
            book.ReleaseYear = updatebookDTO.ReleaseYear;

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDTO)
        {
            var book = new Book()
            {
                Title = bookDTO.Title,
                ISBN = bookDTO.ISBN,
                ReleaseYear = bookDTO.ReleaseYear,
                Available = "true",
                Authors = bookDTO.Authors
            };

            var bookauthor = book?.Authors?.Select(b => b.AuthorIdentity).ToList();
            var authors = _context.Authors.Select(b => b.AuthorIdentity).ToList();

            if (bookauthor is not null)
            {
                if (bookauthor.Intersect(authors).Any())
                {
                    return BadRequest
                        ("Error - author already exists. " +
                        "To add existing author, please do so, after creating the book(without author), " +
                        "by updating the book.");
                }
            }

            if (book is null)
            {
                return NotFound();
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
        private bool BookLoanExists(int id)
        {
            return _context.LibraryLoans.Any(e => e.BookId == id);
        }
    }
}
