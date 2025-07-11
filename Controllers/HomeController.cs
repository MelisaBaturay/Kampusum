using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Starter()
        {
            return View();
        }
        public IActionResult StudentLife()
        {
            return View();
        } 
        public IActionResult Contact()
        {
            return View();
        }

    }
}
