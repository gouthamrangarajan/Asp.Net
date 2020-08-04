using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSuggestUsingTrie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private CountriesTrie _countries;

        public SearchController(CountriesTrie countries)
        {
            _countries = countries;
        }

        [HttpGet]
        public async Task<List<string>> Get(string query)
        {
            var dt=await Task.Factory.StartNew<List<string>>(() =>
            {
                return _countries.Search(query);
            });

            return dt;
        }

    }
}
