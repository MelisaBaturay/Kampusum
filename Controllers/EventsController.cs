using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
