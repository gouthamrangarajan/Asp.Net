using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace RoutesSearchUsingGraph
{
    public class Trie
    {
        private Node _root = new Node();

        public void AddString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            value = value.ToUpper();
            _root.Add(value);
        }

        public IEnumerable<string> Search(string value)
        {
            var retList = new List<string>();
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.ToUpper();
                _root.FindAll(value, retList);
            }
            return retList;
        }

        private class Node
        {
            private Dictionary<char, Node> _children = new Dictionary<char, Node>();
            private bool _isCompleted;
            internal void Add(string value)
            {
                recursiveAdd(value, 0);
            }
            private void recursiveAdd(string value,int index)
            {
                if (index >= value.Length)
                {
                    _isCompleted = true;
                    return;
                }

                var child = getChild(value[index]);
                if(child==null)
                {
                    child = new Node();
                    setChild(value[index], child);
                }
                child.recursiveAdd(value, index + 1);
            }

            private void setChild(char c,Node n)
            {
                _children.Add(c, n);
            }
            private Node getChild(char c)
            {
                if (_children.ContainsKey(c))                
                    return _children[c];

                return null;
            }

            internal void FindAll(string value, List<string> retList)
            {
                recursiveFind(value, retList, 0);
            }

            private void recursiveFind(string value, List<string> retList, int index)
            {                
                if (index >= value.Length)
                {
                    collectedCompleted(retList,value);
                    return;
                }
                var child = getChild(value[index]);
                if (child != null)
                {
                    child.recursiveFind(value, retList, index + 1);
                }
            }

            private void collectedCompleted(List<string> retList, string prefix)
            {
                if (_isCompleted)
                    retList.Add(prefix);

                foreach (var child in _children)
                    child.Value.collectedCompleted(retList, prefix + child.Key);
            }
        }
    }
}
