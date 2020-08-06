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
        private Trie _trie;

        public RouteController(Graph graph,Trie trie)
        {
            _graph = graph;
            _trie = trie;
        }

        [HttpPost("add")]
        public void Add(RouteData routeData)
        {
            _graph.AddRoute(routeData.Source, routeData.Destination);
            _trie.AddString(routeData.Source);
            _trie.AddString(routeData.Destination);
        }
        [HttpGet("destination/search")]
        public IEnumerable<string> SearchDestination(string query)
        {
            return _trie.Search(query);
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
