using Microsoft.AspNetCore.Mvc;
using Kampusum.Models;

namespace Kampusum.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Starter()
        {
            var newsList = _context.Newss.ToList();
            return View(newsList);
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
