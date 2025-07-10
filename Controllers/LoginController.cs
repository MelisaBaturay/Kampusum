using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        } 
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
