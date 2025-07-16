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
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
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

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete()
        {
            var newsList = _context.Newss.ToList();
            return View(newsList);
        }
        [Authorize(Policy = "AdminOnly")]

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var newsItem = _context.Newss.Find(id);
            if (newsItem != null)
            {
                _context.Newss.Remove(newsItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Delete");
        }
    }
}
