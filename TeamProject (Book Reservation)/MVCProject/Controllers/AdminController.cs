using Microsoft.AspNetCore.Mvc;
using MVCProject.Config;

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
