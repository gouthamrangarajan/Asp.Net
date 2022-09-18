using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MyApp.Namespace
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;
        private ILogger<IndexModel> _logger;
        private IUserStore<IdentityUser> _userStore;

        public IndexModel(SignInManager<IdentityUser> signInManager,ILogger<IndexModel> logger,IUserStore<IdentityUser> userStore)
        {
            _signInManager=signInManager;           
            _logger=logger; 
            _userStore=userStore;
        }

        public void OnGet()
        {
         
        }
        public async Task<IActionResult> OnPost(string provider)
        {         
            var  data=await _signInManager.GetExternalAuthenticationSchemesAsync();            
            var redirectUrl = Url.Page("./Index", pageHandler: "Callback");            
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);            
            return new ChallengeResult(provider, properties);
        }        
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {            
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                _logger.LogWarning("Call back from External Provider: Information is empty");                
                return RedirectToPage("./");
            }                      
            _logger.LogInformation("Call back from External Provider: Received Claims");
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier=>XXXXXXXXXXXXXXXXXXXXXXXXX
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name=>rgoutham raja
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname=>rgoutham
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname=>raja
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress=>goutham.master@gmail.com


            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier=>XXXXXXXXXXXXXXXXXXXXXXXXXX
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name=>Goutham Rangarajan
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname=>Goutham
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname=>Rangarajan
            // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress=>grangarajan@ana-data.com
            var user=new IdentityUser{
                Email=info.Principal.Claims.First(f=>f.Type==System.Security.Claims.ClaimTypes.Email).Value,    
                Id=info.Principal.Claims.First(f=>f.Type==System.Security.Claims.ClaimTypes.NameIdentifier).Value,    
                UserName=info.Principal.Claims.First(f=>f.Type==System.Security.Claims.ClaimTypes.Email).Value
            };            
            var T1= _signInManager.SignInWithClaimsAsync(user,true,info.Principal.Claims);
            var T2=_userStore.CreateAsync(user,System.Threading.CancellationToken.None);
            Task.WaitAll(T1,T2);            
            return RedirectToPage("./Auth");         
        }
        
    }
}
