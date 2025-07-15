using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
