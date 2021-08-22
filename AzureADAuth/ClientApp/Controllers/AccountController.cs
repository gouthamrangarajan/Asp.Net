using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientApp.Controllers{
    public class AccountController : ControllerBase
    {
        public IActionResult SignedOut()
        {
            return RedirectToRoute("/SignOut");
        }
    }
}