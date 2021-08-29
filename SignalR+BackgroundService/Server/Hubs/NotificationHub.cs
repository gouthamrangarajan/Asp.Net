using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SignalR_BackgroundService.Hubs{
    public interface INotification{
        Task Notify(dynamic data);
        Task NotifyAll(dynamic data);
    }
    public class NotificationHub:Hub<INotification>{
        private ConnectedUsers _connectedUsers;
        private ILogger<NotificationHub> _logger;

        public NotificationHub(ConnectedUsers connectedUsers,ILogger<NotificationHub>logger){
            _connectedUsers=connectedUsers;
            _logger=logger;
        }

       public override Task OnConnectedAsync()
      {          
          _connectedUsers.AddConnection(Context.ConnectionId);
         _logger.LogInformation($"context connected {Context.ConnectionId}");
          return  base.OnConnectedAsync();          
      }
       public override Task OnDisconnectedAsync(Exception e)
      {
          _connectedUsers.RemoveConnection(Context.ConnectionId);   
          _logger.LogInformation($"context disconnected {Context.ConnectionId}");
          return base.OnDisconnectedAsync(e);          
      } 
    }
}