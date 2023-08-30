using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using htmx101.Models;
using htmx101.Models.JsonPlaceholder;
using Htmx;

namespace htmx101.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IEnumerable<UserViewModel>? _vmCache=null;
    private static HttpClient httpClient=new HttpClient();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public async Task<IActionResult> Index(){        
        (var srchTrm,var users)=await getNewOrCachedData();
        if(Request.IsHtmx())
            return PartialView("/Views/Partials/_UserTable.cshtml",users);
        else
            return View(new FilterViewModel{Users=users,SearchTxt=srchTrm});
    }
       
    private async Task<(string srchTrm,IEnumerable<UserViewModel> users)> getNewOrCachedData(){
        (string srchTrm,IEnumerable<UserViewModel> users)=("",Array.Empty<UserViewModel>());
                 
        if(Request.QueryString.Value!=null && Request.QueryString.Value.ToLower().Contains("search"))
            srchTrm= Request.Query.First(f=>f.Key=="search").Value.ToString().ToLower();

         try{       
            _vmCache??=await httpClient.GetFromJsonAsync<IEnumerable<UserViewModel>>("https://jsonplaceholder.typicode.com/users");
        }   
        catch(Exception e){
            _logger.LogError("Error calling json placeholder api",e);           
        }
        
        if(_vmCache is not null)
            users=_vmCache.Where(f=>f.UserName.ToLower().Contains(srchTrm) || f.Name.ToLower().Contains(srchTrm)
                                 || f.Website.ToLower().Contains(srchTrm) || f.Phone.Contains(srchTrm));
        return (srchTrm,users);
    }
}
