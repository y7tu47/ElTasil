using ElTasilAcademy.Models;
using ElTasilAcademy.servicies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElTasilAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController(MediaManagerService _mediaManagerService,
        ApplicationDbContext _context,
        AudioProcessorsService _audioProcessorsService,
        IWebHostEnvironment env,
        ILogger<LessonController> logger) : ControllerBase
    {

        // GET: api/Lesson
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _context.Lesson.ToListAsync();
        }

        [HttpGet("GetLessonsByCourseId")]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessonsByCourseId(int courseId)
        {
            return await _context.Lesson
            .Where(l => l.CourseId == courseId)
            .ToListAsync();
            
        }


        // GET: api/Lesson/5
        [HttpGet("GetLessonMetaData")]
        public async Task<ActionResult<Lesson>> GetLessonMetaData(int id)
        {
            var lesson = await _context.Lesson.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }

        [HttpGet("GetLesson")]
        public async Task<ActionResult<Lesson>> GetLesson(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "audio/opus", enableRangeProcessing: true);
        }

        // PUT: api/Lesson/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(int id, Lesson lesson)
        {
            if (id != lesson.LessonId)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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

        // POST: api/Lesson
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostLesson")]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)] 
        [RequestSizeLimit(209715200)]
        public async Task<ActionResult> PostLesson()
        {
            logger.LogInformation("logging");

            //"C:\\Users\\drmad\\OneDrive\\Documents\\Images\\" + Guid.NewGuid() + ".m4a"
            var uploadFolder = Path.Combine(env.ContentRootPath, "App_Data", "Uploads");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + ".opus";
            var UploadFilePath = Path.Combine(uploadFolder, uniqueFileName);

            _mediaManagerService.UploadFilePath = UploadFilePath;

            logger.LogInformation(UploadFilePath);
            logger.LogInformation("logging");

            if (!Request.ContentType?.StartsWith("multipart/form-data") ?? true)
            {
                return BadRequest("The request does not contain valid multipart form data.");
            }

            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
            if (string.IsNullOrWhiteSpace(boundary))
            {
                return BadRequest("Missing boundary in multipart form data.");
            }

            var cancellationToken = HttpContext.RequestAborted;
            var filePath = await _mediaManagerService.SaveViaMultipartReaderAsync(boundary, Request.Body, cancellationToken);
            //await _audioProcessorsService.ProcessAudioFileAsync(filePath);
            return Ok(new { filePath = UploadFilePath });

        }

        [HttpPost("PostLessonMetaData")]
        public async Task<ActionResult> PostLessonMetaData(Lesson lesson)
        {
            logger.LogInformation("logging PostLessonMetaData");

            if (!System.IO.File.Exists(lesson.FilePath))
            {
                return NotFound("Associated file not found.");
            }

            _context.Lesson.Add(lesson);
            await _context.SaveChangesAsync();

            return Ok();

        }

        // DELETE: api/Lesson/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _context.Lesson.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonExists(int id)
        {
            return _context.Lesson.Any(e => e.LessonId == id);
        }
    }
}
