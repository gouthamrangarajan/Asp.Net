using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using htmx101.Models;
using htmx101.Models.JsonPlaceholder;

namespace htmx101.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private IEnumerable<UserViewModel>? _vmCache=null;
    private static HttpClient httpClient= new HttpClient();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {        
        _vmCache??=await callApi();
        return View(_vmCache);
    }
    private async Task<IEnumerable<UserViewModel>?> callApi(){
        try{
            _logger.LogInformation("Calling json placeholder api");
            return await httpClient.GetFromJsonAsync<IEnumerable<UserViewModel>>("https://jsonplaceholder.typicode.com/users");
        }   
        catch(Exception e){
            _logger.LogError("Error calling json placeholder api",e);
            return null;
        }
    }
    public async Task<IActionResult> Filter(){
        var httpClient=new HttpClient();     
        IEnumerable<UserViewModel> vm=new List<UserViewModel>();   
        var srchTrm="";
        if(Request.QueryString.Value!=null && Request.QueryString.Value.ToLower().Contains("search"))
            srchTrm= Request.Query.First(f=>f.Key=="search").Value.ToString().ToLower();

        _vmCache??=await callApi();
        if(_vmCache is not null)
            vm=_vmCache.Where(f=>f.UserName.ToLower().Contains(srchTrm) || f.Name.ToLower().Contains(srchTrm)
                                 || f.Website.ToLower().Contains(srchTrm) || f.Phone.Contains(srchTrm));
        return PartialView("/Views/Partials/_UserTable.cshtml",vm);
    }
}
