using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_BackgroundService.Hubs{
    public interface INotification{
        Task Notify(dynamic data);
        Task NotifyAll(dynamic data);
    }
    public class NotificationHub:Hub<INotification>{
       
    }
}