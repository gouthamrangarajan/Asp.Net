using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClientApp.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Microsoft.Identity.Web;

namespace ClientApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDownstreamWebApi _downstreamWebApi;        

        public IndexModel(ILogger<IndexModel> logger,IDownstreamWebApi downstreamWebApi)
        {
            _logger = logger;
            _downstreamWebApi = downstreamWebApi;            
        }

        public IEnumerable<WeatherForecast> WeatherForeCastData;

        [BindProperty(SupportsGet = true)]
        public bool Refresh{get;set;}
        public async Task OnGetAsync()
        {
            if(Refresh){                                                           
                this.WeatherForeCastData=await _downstreamWebApi.CallWebApiForUserAsync<IEnumerable<WeatherForecast>>("WebApi",options=>{
                    options.HttpMethod=HttpMethod.Get;
                    options.RelativePath="weatherforecast";                                        
                });                            
            }
        }             
    }
}
