using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Model.Entities;
using static LibraryAPI.Model.DTOs.LoanDTOs;
using LibraryAPI.Model;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLibraryLoans()
        {
            return await _context.LibraryLoans.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _context.LibraryLoans.FindAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        [HttpPut("ReturnBook/{id}")]
        public async Task<IActionResult> PutLoan(int id, ReturnBookDTO returnDTO)
        {
            var loan = await _context.LibraryLoans.FindAsync(id);

            if (loan is null)
            {
                return NotFound();
            }

            if (id != loan.Id)
            {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            loan.ReturnDate = DateTime.Now;
            loan.Available = true;
            loan.Rating = returnDTO.Rating;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
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

        [HttpPost("BorrowBook")]
        public async Task<ActionResult> PostLoan(BorrowBookDTO loanDTO)
        {

            var loan = new Loan()
            {
                BookId = loanDTO.BookId,
                LibraryUserId = loanDTO.LibraryUserId,
                LoanDate = DateTime.Now

            };

            if (LoanExists(loan.BookId))
            {
                return BadRequest("The book is not available");
            }

            _context.LibraryLoans.Add(loan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoanExists(loan.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLoan(int id)
        //{
        //    var loan = await _context.LibraryLoans.FindAsync(id);
        //    if (loan == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.LibraryLoans.Remove(loan);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool LoanExists(int id)
        {
            return _context.LibraryLoans.Any(e => e.BookId == id && e.Available == false); //e.LoanDate.HasValue
        }
    }
}
