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
        private LoremIpsumTrie _loremIpsumTrie;

        public SearchController(CountriesTrie countries,LoremIpsumTrie loremIpsumTrie)
        {
            _countries = countries;
            _loremIpsumTrie = loremIpsumTrie;
        }

        [HttpGet("country")]
        public IEnumerable<string> Get(string query)
        {            
          return _countries.Search(query);            
        }

        [HttpGet("loremipsum")]
        public IEnumerable<string> GetLoremIpsum(string query)
        {
            return _loremIpsumTrie.Search(query);

        }

    }
}
