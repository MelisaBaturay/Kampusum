using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string pass)
        {
            if (username == "admin" && pass == "melisa")
            {
                return RedirectToAction("Panel");
            }

            ViewBag.LoginError = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
        public IActionResult Panel()
        {
            return View();
        }
    }
}
