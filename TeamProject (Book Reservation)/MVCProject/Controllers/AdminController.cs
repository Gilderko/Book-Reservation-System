using Microsoft.AspNetCore.Mvc;
using MVCProject.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Main()
        {
            if (!User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return NotFound();
            }

            return View();
        }
    }
}
