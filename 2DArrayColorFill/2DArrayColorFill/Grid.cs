using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace _2DArrayColorFill
{
    public class Grid
    {
        private static int dimension = 12;
        private Cell[,] matrix = new Cell[dimension,dimension];

        public Grid()
        {
            for(var rowNum = 0; rowNum <= matrix.GetUpperBound(0); rowNum++)
            {
                for (var colNum = 0; colNum <= matrix.GetUpperBound(1); colNum++)
                {
                    matrix[rowNum, colNum] = new Cell();
                }
            }
        }

        public int GetDimension()
        {
            return dimension;
        }

        public IEnumerable<Tuple<int,int>> GetBlockedOnlyCells()
        {
            var retList = new List<Tuple<int, int>>();
            for (var rowNum = 0; rowNum <= matrix.GetUpperBound(0); rowNum++)
            {
                for (var colNum = 0; colNum <= matrix.GetUpperBound(1); colNum++)
                {
                    if(matrix[rowNum,colNum].IsBlocked() && !matrix[rowNum, colNum].TryGetColor(out string test))
                    {
                        retList.Add(new Tuple<int, int>(rowNum, colNum));
                    }
                }
            }
            return retList;
        }

        public IEnumerable<Tuple<int,int,string>> GetCellColors()
        {
            var retList = new List<Tuple<int, int,string>>();
            for (var rowNum = 0; rowNum <= matrix.GetUpperBound(0); rowNum++)
            {
                for (var colNum = 0; colNum <= matrix.GetUpperBound(1); colNum++)
                {
                    if ( matrix[rowNum, colNum].TryGetColor(out string color))
                    {
                        retList.Add(new Tuple<int, int,string>(rowNum, colNum,color));
                    }
                }
            }
            return retList;
        }

        private bool isValidDimension(int rowNum,int colNum)
        {
            return rowNum>-1 && colNum>-1 && matrix.GetUpperBound(0) >= rowNum && matrix.GetUpperBound(1) >= colNum;
        }

        public bool BlockLocation(Tuple<int,int> dimension)
        {
            if (isValidDimension(dimension.Item1, dimension.Item2))
            {
                matrix[dimension.Item1, dimension.Item2].Block();
            }
            return false;
        }

        public bool UnBlockLocation(Tuple<int, int> dimension)
        {
            if (isValidDimension(dimension.Item1, dimension.Item2))
            {
                matrix[dimension.Item1, dimension.Item2].UnBlock();
                return true;
            }
            return false;
        }

        public bool EraseColor(Tuple<int, int> dimension)
        {
            if(isValidDimension(dimension.Item1, dimension.Item2)                   
                   && matrix[dimension.Item1,dimension.Item2].TryGetColor(out string test))
            {
                matrix[dimension.Item1, dimension.Item2].RemoveColor();
                return true;
            }
            return false;
        }

        public void SetColor(Tuple<int,int> dimension,string color)
        {                        
            recursiveSetColor(dimension, color);            
        }

        

        private void recursiveSetColor(Tuple<int, int> dimension, string color)
        {
            if (!isValidDimension(dimension.Item1, dimension.Item2) 
              || matrix[dimension.Item1, dimension.Item2].IsBlocked())
                return;

            matrix[dimension.Item1, dimension.Item2].SetColor(color);

            for (var rowNum = dimension.Item1 - 1; rowNum <= dimension.Item1 + 1; rowNum++)
            {
                for(var colNum = dimension.Item2 - 1; colNum <= dimension.Item2 + 1; colNum++)
                {
                    if (rowNum != dimension.Item1 || colNum != dimension.Item2)
                    {                       
                        recursiveSetColor(new Tuple<int, int>(rowNum, colNum), color);
                    }
                }
            }
        }

        private class Cell
        {
            private bool _isBlocked;
            private string _color;

            internal void Block()
            {
                _isBlocked = true;
            }

            internal void UnBlock()
            {
                _isBlocked = false;
            }

            internal bool IsBlocked()
            {
                return _isBlocked;
            }

            internal void SetColor(string color)
            {
                _color = color;
                _isBlocked = true;
            }

            internal void RemoveColor()
            {
                _color = null;
                _isBlocked = false;
            }
            internal bool TryGetColor(out string color)
            {
                color = null;
                if (_color != null)
                {
                    color = _color;
                    return true;
                }
                return false;
            }
        }
    }
}
