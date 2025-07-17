using Kampusum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kampusum.Controllers
{
    public class EventsController : Controller
    {
        private readonly Context _context;
        private readonly EmailService _emailService;
        public EventsController(Context context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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

            var viewModel = new EventDetailsViewModel
            {
                Event = eventDetail,
                Registration = new EventRegistration { EventId = id }
            };

            return View(viewModel);
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(EventRegistration registration)
        {
            if (ModelState.IsValid)
            {
                _context.EventRegistrations.Add(registration);
                _context.SaveChanges();

                var eventDetail = _context.Events.FirstOrDefault(e => e.EventId == registration.EventId);

                var subject = "Etkinlik Kaydınız Başarılı!";
                var body = $@"
            <h2>Etkinlik Kaydınız Başarıyla Tamamlandı</h2>
            <p>Etkinlik: <b>{eventDetail.Title}</b></p>
            <p>Açıklama: {eventDetail.Description}</p>
            <p>Tarih: {eventDetail.Date:dd.MM.yyyy}</p>
            <p>Saat: {eventDetail.StartTime.ToString(@"hh\:mm")} - {eventDetail.EndTime.ToString(@"hh\:mm")}</p>            
            <p>Yer: {eventDetail.Location}</p>
            <br>
            <p>Katılımınız için teşekkürler!</p>
        ";

                await _emailService.SendEmailAsync(registration.Email, subject, body);

                TempData["SuccessMessage"] = "Kayıt başarılı! Bilgileriniz e-posta adresinize gönderildi.";
                return RedirectToAction("EventsDetails", new { id = registration.EventId });
            }

            TempData["ErrorMessage"] = "Kayıt başarısız. Lütfen formu kontrol edin.";

            var eventDetail2 = _context.Events
                .Include(e => e.Schedules)
                .FirstOrDefault(e => e.EventId == registration.EventId);

            if (eventDetail2 == null)
                return NotFound();

            var viewModel = new EventDetailsViewModel
            {
                Event = eventDetail2,
                Registration = registration
            };

            return View("EventsDetails", viewModel);
        }
    }
}
