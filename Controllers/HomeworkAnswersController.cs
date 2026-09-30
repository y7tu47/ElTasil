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
    public class HomeworkAnswersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeworkAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HomeworkAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeworkAnswer>>> GetHomeworkAnswer()
        {
            return await _context.HomeworkAnswer.ToListAsync();
        }

        // GET: api/HomeworkAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkAnswer>> GetHomeworkAnswer(int id)
        {
            var homeworkAnswer = await _context.HomeworkAnswer.FindAsync(id);

            if (homeworkAnswer == null)
            {
                return NotFound();
            }

            return homeworkAnswer;
        }

        // PUT: api/HomeworkAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomeworkAnswer(int id, HomeworkAnswer homeworkAnswer)
        {
            if (id != homeworkAnswer.HomeworkAnswerId)
            {
                return BadRequest();
            }

            _context.Entry(homeworkAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeworkAnswerExists(id))
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

        // POST: api/HomeworkAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HomeworkAnswer>> PostHomeworkAnswer(HomeworkAnswer homeworkAnswer)
        {
            _context.HomeworkAnswer.Add(homeworkAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomeworkAnswer", new { id = homeworkAnswer.HomeworkAnswerId }, homeworkAnswer);
        }

        // DELETE: api/HomeworkAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeworkAnswer(int id)
        {
            var homeworkAnswer = await _context.HomeworkAnswer.FindAsync(id);
            if (homeworkAnswer == null)
            {
                return NotFound();
            }

            _context.HomeworkAnswer.Remove(homeworkAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HomeworkAnswerExists(int id)
        {
            return _context.HomeworkAnswer.Any(e => e.HomeworkAnswerId == id);
        }
    }
}
