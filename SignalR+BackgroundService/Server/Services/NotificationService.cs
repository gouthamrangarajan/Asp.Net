using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalR_BackgroundService.Hubs;

namespace SignalR_BackgroundService.Services{
    public class NotificationService : BackgroundService
    {
        
        private ILogger<NotificationService> _logger;
        private IHubContext<NotificationHub, INotification> _hub;
        private ConnectedUsers _connectedUsers;

        public NotificationService(ILogger<NotificationService> logger, IHubContext<NotificationHub, INotification> hub,ConnectedUsers connectedUsers)
        {
            _logger = logger;
            _hub = hub;
            _connectedUsers=connectedUsers;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                 if(_connectedUsers.Cache.Count!=0){       
                    var randConnectionInd= new Random().Next(0, _connectedUsers.Cache.Count);
                    var randConnection=_connectedUsers.Cache.ToList()[randConnectionInd];
                    _logger.LogInformation($"Sending info to {randConnection}");
                    await _hub.Clients.Client(randConnection).Notify(new {msg=$"Hello user ${randConnection}, sending random message",id=new Random().Next(1,1000000000)});
                }
                await Task.Delay(10000);
            }
        }
    }
}