using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Microsoft.Identity.Web;

namespace AzureAD_GmailAuth.Pages
{
    [Authorize]
    public class AuthModel : PageModel
    {
        private readonly ILogger<AuthModel> _logger;        

        public AuthModel(ILogger<AuthModel> logger)
        {
            _logger = logger;
        }

     
    }
}
