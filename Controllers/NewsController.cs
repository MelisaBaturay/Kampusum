using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
