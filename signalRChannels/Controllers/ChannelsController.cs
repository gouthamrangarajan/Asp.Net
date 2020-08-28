using Microsoft.AspNetCore.Mvc;
using signalRChannels.Models;
using System.Collections.Generic;

namespace signalRChannels.Controllers{

    [ApiController]
    [Route("api/[controller]")] 
    public class ChannelsController:ControllerBase{
        private ChannelsInfoCollection _collection;

        public ChannelsController(ChannelsInfoCollection collection){
                _collection=collection;
        }
        [HttpGet]
        public IEnumerable<ChannelsInfoCollection.ChannelsInfo> Get(){
            return _collection.GetData();
        }
    }   
}