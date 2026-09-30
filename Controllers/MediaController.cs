using ElTasilAcademy.Models;
using ElTasilAcademy.servicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ElTasilAcademy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly MediaManagerService _mediaManagerService;
        private readonly AudioProcessorsService _audioProcessorsService;

        public MediaController(MediaManagerService mediaManagerService, AudioProcessorsService audioProcessorsService)
        {
            _mediaManagerService = mediaManagerService;
            _audioProcessorsService = audioProcessorsService;
        }

        [HttpGet]
        [Route("media")]
        public async Task<IActionResult> GetMedia(string fileName)
        {
            var videoPath = Path.Combine("C:\\Users\\drmad\\OneDrive\\Documents\\lecs", fileName);

            if (!System.IO.File.Exists(videoPath))
            {
                return NotFound();
            }

            return PhysicalFile(videoPath, "video/ogg", enableRangeProcessing: true);
        }

        [HttpGet]
        [Route("mediaMetaData")]
        public async Task<IActionResult> GetMediaMetaData()
        {
            return Ok();
        }

        [HttpPost]
        [Route("UploadMedia")]
        [Authorize]
        public async Task<IActionResult> UploadMedia()
        {
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
            _mediaManagerService.UploadFilePath = "C:\\Users\\drmad\\OneDrive\\Documents\\Images\\" + Guid.NewGuid() + ".mp4";
            var filePath = await _mediaManagerService.SaveViaMultipartReaderAsync(boundary, Request.Body, cancellationToken);
            await _audioProcessorsService.ProcessAudioFileAsync(filePath);
            return Ok("Saved file at " + filePath);
        }        
    }
}
