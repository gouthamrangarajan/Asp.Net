using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2DArrayColorFill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatrixController : ControllerBase
    {
        private Grid _grid;

        public MatrixController(Grid grid)
        {
            _grid = grid;
        }

        [HttpGet("dimension")]
        public int Dimension()
        {
            return _grid.GetDimension();
        }

        [HttpGet("cell/block")]
        public IEnumerable<Tuple<int,int>> BlockedCells()
        {
            return _grid.GetBlockedOnlyCells();
        }

        [HttpGet("cell/color")]
        public IEnumerable<Tuple<int, int,string>> CellColors()
        {
            return _grid.GetCellColors();
        }
        
        [HttpPost("cell/color/erase")]
        public bool CellColors(RequestData data)
        {
            return _grid.EraseColor(new Tuple<int, int>(data.Row, data.Col));
        }

        [HttpPost("cell/block")]
        public bool BlockCell(RequestData data)
        {
            return _grid.BlockLocation(new Tuple<int, int>(data.Row, data.Col));
        }

        [HttpPost("cell/block/remove")]
        public bool UnBlockCell(RequestData data)
        {
            return _grid.UnBlockLocation(new Tuple<int, int>(data.Row, data.Col));
        }

        [HttpPost("cell/fill")]
        public void Fill(RequestData data)
        {
            _grid.SetColor(new Tuple<int, int>(data.Row, data.Col), data.Color);
        }
    }
    public class RequestData
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Color { get; set; }
    }
}
