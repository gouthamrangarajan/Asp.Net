using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace RoutesSearchUsingGraph.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private Graph _graph;

        public RouteController(Graph graph)
        {
            _graph = graph;
        }

        [HttpPost("add")]
        public void Add(RouteData routeData)
        {
            _graph.AddRoute(routeData.Source, routeData.Destination);
        }
        [HttpGet("name")]
        public IEnumerable<string> GetAllNames()
        {
            return _graph.GetAllNames();
        }

        [HttpGet("search/one")]
        public string GetShortPath(string source,string destination)
        {
            return _graph.GetShortRoute(source, destination);
        }

        [HttpGet("search/all")]
        public IEnumerable<string> GetAllPath(string source,string destination)
        {
            return _graph.GetAllRoutes(source, destination);
        }
    }
    public class RouteData
    {
        public string Source { get; set; }

        public string Destination { get; set; }
    }
}
