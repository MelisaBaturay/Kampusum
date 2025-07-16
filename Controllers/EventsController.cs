using Kampusum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kampusum.Controllers
{
    public class EventsController : Controller
    {
        private readonly Context _context;
        public EventsController(Context context)
        {
            _context = context;
        }
        public IActionResult Index(string search)
        {

            var eventCards = _context.EventCards.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                eventCards = eventCards.Where(e =>
                    e.Title.Contains(search) ||
                    e.Description.Contains(search) ||
                    e.Location.Contains(search)
                );
            }

            return View(eventCards.ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult EventsDetails(int id)
        {
            var eventDetail = _context.Events
                .Include(e => e.Schedules)
                .FirstOrDefault(e => e.EventId == id);

            if (eventDetail == null)
                return NotFound();

            return View(eventDetail);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public IActionResult Create(Event model)
        {
            var eventCard = new EventCard
            {
                Title = model.Title,
                Location = model.Location,
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Description = model.Description
            };

            _context.EventCards.Add(eventCard);
            _context.SaveChanges();

            if (ModelState.IsValid)
            {
                _context.Events.Add(model);
                _context.SaveChanges();

                // ID artık burada
                return RedirectToAction("EventsDetails", "Events", new { id = model.EventId });
            }

            return View(model);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult Delete()
        {
            var eventList = _context.Events.ToList();
            return View(eventList);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var eventItem = _context.Events.Find(id);
            if (eventItem != null)
            {
                _context.Events.Remove(eventItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Delete");
        }
    }
}
