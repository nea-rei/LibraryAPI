using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Model.Entities;
using LibraryAPI.Model;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryUsersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LibraryUsersController(LibraryDbContext context)
        {
            _context = context;
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<LibraryUser>>> GetLibraryUsers()
        //{
        //    return await _context.LibraryUsers
        //        .Include(l => l.Loans)
        //        .ToListAsync();
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryUser>> GetLibraryUser(int id)
        {
            var libraryUser = await _context.LibraryUsers.FindAsync(id);

            if (libraryUser == null)
            {
                return NotFound();
            }

            return libraryUser;
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLibraryUser(int id, LibraryUser libraryUser)
        //{
        //    if (id != libraryUser.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(libraryUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LibraryUserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost]
        public async Task<ActionResult<LibraryUser>> PostLibraryUser(LibraryUser libraryUser)
        {
            _context.LibraryUsers.Add(libraryUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibraryUser), new { id = libraryUser.Id }, libraryUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibraryUser(int id)
        {
            var libraryUser = await _context.LibraryUsers.FindAsync(id);
            if (libraryUser == null)
            {
                return NotFound();
            }

            _context.LibraryUsers.Remove(libraryUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibraryUserExists(int id)
        {
            return _context.LibraryUsers.Any(e => e.Id == id);
        }
    }
}
