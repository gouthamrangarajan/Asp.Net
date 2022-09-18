using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureAD_GmailAuth
{
    public class SignOutModel : PageModel
    {
        public async Task OnGet()
        {            
            if(HttpContext.User!=null){         
                await HttpContext.SignOutAsync();                
                HttpContext.User=null;        
                HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
                HttpContext.Response.Cookies.Delete("Identity.ExternalC1");                        
                HttpContext.Response.Cookies.Delete("Identity.ExternalC2");                        
                HttpContext.Response.Cookies.Delete("Identity.External");                        
            }
        }
    }
}
