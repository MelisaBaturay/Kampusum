using Kampusum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kampusum.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly Context _context;

        public AnnouncementController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _context.Announcements.Add(announcement);
                _context.SaveChanges();
                return RedirectToAction("Panel", "Admin");
            }
            return View(announcement);
        }
        public IActionResult Index()
        {
            var announcements = _context.Announcements.OrderByDescending(a => a.Date).ToList();
            return View(announcements);
        }
        public IActionResult Announcement()
        {
            var model = _context.Announcements.ToList();
            return View(model);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete()
        {
            var announcements = _context.Announcements.OrderByDescending(a => a.Date).ToList();
            return View(announcements);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var announcement = _context.Announcements.Find(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                _context.SaveChanges();
            }
            return RedirectToAction("Delete");
        }
    }
}