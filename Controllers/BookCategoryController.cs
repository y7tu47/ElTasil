using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElTasilAcademy.Models;

namespace ElTasilAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BookCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCategory>>> GetBookCategory()
        {
            return await _context.BookCategory.ToListAsync();
        }

        // GET: api/BookCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookCategory>> GetBookCategory(int id)
        {
            var bookCategory = await _context.BookCategory.FindAsync(id);

            if (bookCategory == null)
            {
                return NotFound();
            }

            return bookCategory;
        }

        // PUT: api/BookCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookCategory(int id, BookCategory bookCategory)
        {
            if (id != bookCategory.BookCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(bookCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCategoryExists(id))
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

        // POST: api/BookCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCategory>> PostBookCategory(BookCategory bookCategory)
        {
            _context.BookCategory.Add(bookCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookCategory", new { id = bookCategory.BookCategoryId }, bookCategory);
        }

        // DELETE: api/BookCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCategory(int id)
        {
            var bookCategory = await _context.BookCategory.FindAsync(id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            _context.BookCategory.Remove(bookCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookCategoryExists(int id)
        {
            return _context.BookCategory.Any(e => e.BookCategoryId == id);
        }
    }
}
