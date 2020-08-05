using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RoutesSearchUsingGraph
{
    public class Graph
    {
        private Dictionary<string,Node> _nodeLookup=new Dictionary<string,Node>();

        private Node  GetNode(string name)
        {
            if (_nodeLookup.ContainsKey(name))
                return _nodeLookup[name];
            else
                return null;
        }
        public void AddRoute(string source,string destination)
        {
            if(string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(destination))           
                return;

            source = source.ToUpper();
            destination = destination.ToUpper();

            var srcNode = GetNode(source);
            if (srcNode == null)
            {
                srcNode = new Node(source);
                _nodeLookup.Add(source, srcNode);
            }
            var destNode = GetNode(destination);
            if (destNode == null)
            {
                destNode = new Node(destination);
                _nodeLookup.Add(destination, destNode);
            }
            srcNode.AddAdjacent(destNode);
        }
        
        public IEnumerable<string> GetAllRoutes(string source,string destination)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(destination))
                return null;
            source = source.ToUpper();
            destination = destination.ToUpper();
            var routes = new List<string>();
            GetAllRoutesDFS(GetNode(source), GetNode(destination), routes, new HashSet<string>(),source);
            return routes;
        }

        private void GetAllRoutesDFS(Node source, Node destination, List<string> routes,HashSet<string> visited,string currentRoute)
        {
            if (source == null || destination == null)
                return;

            if (source == destination)
                routes.Add(currentRoute);
            else
            {
                if (visited.Contains(source.Name))
                    return;
                visited.Add(source.Name);
                source.Adjacent.ForEach(adj =>
                {
                    GetAllRoutesDFS(adj, destination, routes, visited, currentRoute + "," + adj.Name);
                });
                visited.Remove(source.Name);
            }

        }
        public string GetShortRoute(string source, string destination)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(destination))
                return null;
            source = source.ToUpper();
            destination = destination.ToUpper();
            return GetRouteBFS(GetNode(source), GetNode(destination));
        }

        private string GetRouteBFS(Node source, Node destination)
        {
            if (source == null || destination == null)
                return null;

            Queue<Tuple<string, Node>> queue = new Queue<Tuple<string, Node>>();
            HashSet<string> visited = new HashSet<string>();
            queue.Enqueue(new Tuple<string, Node>(source.Name, source));
            while (queue.Count > 0)
            {
                var currentRecord = queue.Dequeue();

                if (currentRecord.Item2 == destination)
                    return currentRecord.Item1;

                if (visited.Contains(currentRecord.Item2.Name))
                    continue;

                visited.Add(currentRecord.Item2.Name);

                currentRecord.Item2.Adjacent.ForEach(adj =>
                {
                    queue.Enqueue(new Tuple<string, Node>(currentRecord.Item1 + "," + adj.Name, adj));
                });
            }
            return null;
        }

        public IEnumerable<string> GetAllNames()
        {
            return _nodeLookup.Values.Select(s => s.Name);
        }

        private class Node
        {
            internal List<Node> Adjacent = new List<Node>();
            internal string Name;
            internal Node(string name)
            {
                Name = name;
            }

            internal void AddAdjacent(Node node)
            {
                if (!Adjacent.Contains(node))
                    Adjacent.Add(node);
            }
        }
    }
}
