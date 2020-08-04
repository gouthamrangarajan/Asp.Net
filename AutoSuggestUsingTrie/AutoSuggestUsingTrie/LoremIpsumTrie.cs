using System.Collections.Generic;
using System.Globalization;
using System.IO;

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

        public List<string> Search(string value)
        {
            var retList = new List<string>();
            _root.FindAll(value,retList);
            return retList;
        }

        private class Node
        {
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
                node.addStringRecursive(s, index + 1);
            }

            public void FindAll(string value,List<string> collectedsentences)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                findAllRecurison(value, 0, collectedsentences);
            }

            private void findAllRecurison(string value,int index,List<string> collectedsentences)
            {

                if (index>=value.Length)
                {
                    collectCompletedsentencesRecursion(value, collectedsentences);
                    return;
                }
                var node = GetChild(value.ToLower()[index]);
                if (node == null)
                    return;
                node.findAllRecurison(value, index + 1, collectedsentences);
            }

            private void collectCompletedsentencesRecursion(string prefix, List<string> collectedsentences)
            {
                if (_isCompleted)
                    collectedsentences.Add(textInfo.ToTitleCase(prefix));                

                foreach(var child in _children)                
                    child.Value.collectCompletedsentencesRecursion(prefix + child.Key, collectedsentences);                
            }

         
        }
    }
}
