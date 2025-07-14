using Kampusum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class NewsController : Controller
    {
        private readonly Context _context;

        public NewsController(Context context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                _context.Newss.Add(news);
                _context.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(news);
        }
    }
}
