using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_BackgroundTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private Values _values;

        public ValuesController(Values values)
        {
            _values = values;
        }
        [HttpGet]
        public IList<int> Get()
        {
            return _values.GetData();
        }
    }
}
