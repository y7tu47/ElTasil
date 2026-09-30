
using System.Security.Claims;

namespace ElTasilAcademy.servicies
{
    public class PeriodicNotificationService : BackgroundService
    {
        private readonly OneSignalNotificationService _oneSignalNotificationService;
        public PeriodicNotificationService(OneSignalNotificationService oneSignalNotificationService) 
        {
            _oneSignalNotificationService = oneSignalNotificationService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _oneSignalNotificationService.send();
                await Task.Delay(TimeSpan.FromSeconds(10),stoppingToken);
            }
        }
    }
}
