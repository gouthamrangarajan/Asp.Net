### SignalR service hosted with background services

Sample demo to send notification to Client using SignalR.

- Server & Client belong to different domain & solution
- Server folder contains two functionalities
  - SignalR to send real time notification/data to client
  - A Background service to check if any new data is available and send that info to SignalR service (in the sample its sends dummy message to random connection every 10 seconds)
- Client folder is a simple html & javascript listening to SignalR

The design can be depicted using below \
![design image](https://github.com/gouthamrangarajan/Asp.Net/blob/master/SignalR%2BBackgroundService/Design.png)

### Server Code highlights

##### Startup.cs

1. Add both SignalR and HostedService to ServiceCollection

```C#
//optional cors for local development
services.AddCors(options=>{
    options.AddPolicy("AllowLocalhostClient",builder=>{
        builder.WithOrigins("http://127.0.0.1:5500");
        builder.AllowCredentials();
        builder.AllowAnyHeader();
    });
});
services.AddSingleton<ConnectedUsers>();
services.AddSignalR();
services.AddHostedService<NotificationService>();
```

2. Map Hub to the corresponding path

```C#
//optional cors for local development
app.UseCors("AllowLocalhostClient");
....
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/hubs/notification");
    endpoints.MapGet("/",(context)=>{
        return context.Response.WriteAsync("Get response sent from SignalR hosted in background services");
    });
});
```

##### NotificationHub.cs

- Define Hub. Notify and NotifyAll are the channels/events to be listened by client

```C#
public interface INotification{
    Task Notify(dynamic data);
    Task NotifyAll(dynamic data);
}
public class NotificationHub:Hub<INotification>{
....
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
```

#### NotificationService.cs

- Background Service pushing message/data to hub

```C#
public class NotificationService : BackgroundService
{
...
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
          await _hub.Clients.Client(randConnection).Notify(new {msg=$"Hello user, sending random message",id=new Random().Next(1,1000000000)});
      }
      await Task.Delay(10000);
  }
}
```

#### ConnectedUsers.cs

- simple caching of connection

```C#
public class ConnectedUsers
{
private HashSet<string> _cache=new HashSet<string>();
private ILogger<ConnectedUsers> _logger;

public HashSet<string> Cache => _cache;
public ConnectedUsers(ILogger<ConnectedUsers> logger){
    _logger=logger;
}
public void AddConnection(string connectionId){
    if(!_cache.Contains(connectionId)){
        _cache.Add(connectionId);
    }
}
public void RemoveConnection(string connectionId){
    _cache.Remove(connectionId);
}
```

### Client

UI Screenshot is shown below\
![design image](https://github.com/gouthamrangarajan/Asp.Net/blob/master/SignalR%2BBackgroundService/Client.PNG)

##### javascript code in index.html

```javascript
const useSignalRConnection = (callback) => {
  let connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/hubs/notification")
    .build();
  connection.on("NotifyAll", (info) => {
    callback(info);
  });
  connection.start().catch((err) => {
    console.log(err);
  });
};
```

- Sample UI done using Vue js and Tailwind CSS

Once Authentication is achieved the design can be depicted using below \
![design image](https://github.com/gouthamrangarajan/Asp.Net/blob/master/SignalR%2BBackgroundService/Authentication.png)

### Official Links

[https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-5.0&tabs=visual-studio-code](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-5.0&tabs=visual-studio-code)\
[https://docs.microsoft.com/en-us/aspnet/core/signalr/background-services?view=aspnetcore-5.0](https://docs.microsoft.com/en-us/aspnet/core/signalr/background-services?view=aspnetcore-5.0)\
[https://docs.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-5.0](https://docs.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-5.0)\
[https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-5.0](https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-5.0)
