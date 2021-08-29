using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SignalR_BackgroundService{
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
    }
}