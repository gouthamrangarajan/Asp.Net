using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalR_BackgroundService.Hubs;

namespace SignalR_BackgroundService.Services{
    public class NotificationService : BackgroundService
    {
        private int counter=1;
        private ILogger<NotificationService> _logger;
        private IHubContext<NotificationHub, INotification> _hub;

        public NotificationService(ILogger<NotificationService> logger, IHubContext<NotificationHub, INotification> hub)
        {
            _logger = logger;
            _hub = hub;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hub.Clients.All.NotifyAll(new {msg=$"Sample Message {counter}",id=counter});                
                counter++;
                await Task.Delay(5000);
            }
        }
    }
}