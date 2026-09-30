using ElTasilAcademy.servicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElTasilAcademy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController(OneSignalNotificationService oneSignalNotificationService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        [Route("notify")]
        public async Task<IActionResult> SendPushNotification()
        {
            oneSignalNotificationService.send();
            return Ok();
        }
    }
}
