using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AutoSuggestUsingTrie
{
    public class LoremIpsumTrie
    {
        private Node _root = new Node();        

        public LoremIpsumTrie()
        {
            var allData = File.ReadAllText("Data\\loremipsum.txt");
            allData = allData.Replace("\n", ".");
            foreach (var dt in allData.Split('.'))
            {                
               _root.AddString(dt.ToLower().Trim());
            }
        }

        public IEnumerable<string> Search(string value)
        {
            var retList = new List<string>();
            _root.FindAll(value,retList);                
            return retList.Distinct();
        }

        private class Node
        {
            private static List<Node> spaceNodes = new List<Node>();
            private Dictionary<char, Node> _children = new Dictionary<char, Node>();            
            private bool _isCompleted;
            private static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            public Node GetChild(char c)
            {
                if (_children.ContainsKey(c))
                    return _children[c];
                else
                    return null;
            }

            public void AddString(string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return;

                addStringRecursive(s, 0);
            }
            private void addStringRecursive(string s, int index)
            {
                if (index >= s.Length)
                {
                    _isCompleted = true;
                    return;
                }
                var node = GetChild(s[index]);
                if (node == null)
                {
                    node = new Node();
                    _children[s[index]] = node;
                }
                if(s[index]==' ')
                {
                    spaceNodes.Add(node);
                }
                node.addStringRecursive(s, index + 1);
            }

            public void FindAll(string value,List<string> collectedResults)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                findAllRecurison(value, 0, collectedResults);
                findAllInSpacedNodes(value, collectedResults);
            }

            private void findAllRecurison(string value,int index,List<string> collectedResults)
            {

                if (index>=value.Length)
                {
                    collectCompletedResultsRecursion(value, collectedResults);
                    return;
                }
                var node = GetChild(value.ToLower()[index]);
                if (node == null)
                    return;
                node.findAllRecurison(value, index + 1, collectedResults);
            }

            private void collectCompletedResultsRecursion(string prefix, List<string> collectedResults)
            {
                if (_isCompleted)
                    collectedResults.Add(textInfo.ToTitleCase(prefix.Trim()));

                if (_children.Count == 1 && _children.ContainsKey(' '))
                {
                    collectedResults.Add(textInfo.ToTitleCase(prefix.Trim()));
                }
                else
                {
                    foreach (var child in _children)
                        child.Value.collectCompletedResultsRecursion(prefix + child.Key, collectedResults);
                }
            }

            private static void findAllInSpacedNodes(string value, List<string> collectedResults)
            {
                spaceNodes.ForEach(sn =>
                {
                    sn.findAllRecurison(value,0, collectedResults);
                });

            }


        }
    }
}
