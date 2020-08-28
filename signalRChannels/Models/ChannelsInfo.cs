using System;
using System.Collections.Generic;
using System.Text.Json;

namespace signalRChannels.Models{

    public class ChannelsInfoCollection{
        private Dictionary<string,ChannelsInfo> _data=new Dictionary<string,ChannelsInfo>();
        private int _nextId=2;

        public ChannelsInfoCollection(){
            _data.Add("TEST CHANNEL",
            new ChannelsInfo{Id=1,Name="Test Channel",RecentMsg="Created by System",
            RecentMsgTime=DateTime.Now});
        }
        
        public IEnumerable<ChannelsInfo> GetData(){
            return _data.Values;
        }
        public ChannelsInfo AddChannel(string name,string user){
            if(!_data.ContainsKey(name.ToUpper())){
                _data.Add(name.ToUpper(),new ChannelsInfo{
                    Name=name,
                    Id=_nextId,
                    RecentMsg=$"Channel Created By {user}",
                    RecentMsgTime=DateTime.UtcNow
                });
                _nextId++;
                return _data[name.ToUpper()];
            }            
            return null;
        }

        public override string ToString()=> JsonSerializer.Serialize<IEnumerable<ChannelsInfo>>(_data.Values);
        
        public class ChannelsInfo
        {
            public int Id {get;set;}
            public string Name{get;set;}
            public string RecentMsg{get;set;}

            public DateTime RecentMsgTime{get;set;}

            public override string ToString()=>JsonSerializer.Serialize<ChannelsInfo>(this);
        }
    }

    
}