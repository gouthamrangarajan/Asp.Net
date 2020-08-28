using Microsoft.AspNetCore.SignalR;
using signalRChannels.Models;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace signalRChannels.Hubs
{
    public class ChannelsInfoHub:Hub
    {        
        private readonly ChannelsInfoCollection _channels;
        private readonly UserCollection _users;

        private readonly ILogger<ChannelsInfoHub> _logger;
        public ChannelsInfoHub(ChannelsInfoCollection channels,UserCollection users,ILogger<ChannelsInfoHub> logger){
            _channels=channels;
            _users=users;
            _logger=logger;
        }
        public async Task CreateChannel(string name,string user){
            _logger.LogInformation($"Create Channel {name} received {Context.ConnectionId}");
            if(!string.IsNullOrWhiteSpace(name)){
                var dt= _channels.AddChannel(name,user);
                if(dt!=null){
                    _logger.LogInformation($"Create Channel {name} Success");
                    await Clients.All.SendAsync("ChannelCreated",dt);            
                }
            }
        }
        public override async Task OnConnectedAsync(){
            _logger.LogInformation($"Connection started {Context.ConnectionId}");
            _users.AddUser(Context.ConnectionId);
            await base.OnConnectedAsync();                                                         
        }

        public override async Task OnDisconnectedAsync(Exception exception){            
            _users.RemoveUser(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);                   
        }
    }
}