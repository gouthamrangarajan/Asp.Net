### SignalR service hosted with background services

Sample demo to send notification to Client using SignalR.\
The overrall design is shown below\
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

}
```

#### NotificationService.cs

- Background Service pushing message/data to hub

```C#
public class NotificationService : BackgroundService
{
...
public NotificationService(ILogger<NotificationService> logger, IHubContext<NotificationHub, INotification> hub)
{
    _logger = logger;
    _hub = hub;
}
...
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        await _hub.Clients.All.NotifyAll(new {msg=$"Sample Message {counter}",id=counter});
        counter++;
        await Task.Delay(5000);
    }
}
```

### Client

UI Screenshot is shown below\
![design image](https://github.com/gouthamrangarajan/Asp.Net/blob/master/SignalR%2BBackgroundService/Client_Screenshot.PNG)

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

### Official Links

[https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-5.0&tabs=visual-studio-code](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-5.0&tabs=visual-studio-code)\
[https://docs.microsoft.com/en-us/aspnet/core/signalr/background-services?view=aspnetcore-5.0](https://docs.microsoft.com/en-us/aspnet/core/signalr/background-services?view=aspnetcore-5.0)\
[https://docs.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-5.0](https://docs.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-5.0)\
[https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-5.0](https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-5.0)\
