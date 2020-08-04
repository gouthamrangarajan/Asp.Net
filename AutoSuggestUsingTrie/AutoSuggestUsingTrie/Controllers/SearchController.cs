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
        public async Task<List<string>> Get(string query)
        {
            var dt=await Task.Factory.StartNew<List<string>>(() =>
            {
                return _countries.Search(query);
            });

            return dt;
        }

        [HttpGet("loremipsum")]
        public async Task<List<string>> GetLoremIpsum(string query)
        {
            var dt = await Task.Factory.StartNew<List<string>>(() =>
            {
                return _loremIpsumTrie.Search(query);
            });

            return dt;
        }

    }
}
