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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(contactMessage);
                _context.SaveChanges();
                ViewBag.Success = "Mesajınız başarıyla iletildi.";
                ModelState.Clear();
                return View(new ContactMessage());
            }
            else
            {
                ViewBag.Error = "Lütfen tüm alanları doğru doldurun.";
                return View(contactMessage);
            }
        }
    }
}
