using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
