using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System;
using Kampusum.Models;

namespace Kampusum.Controllers
{
    public class AdminController : Controller
    {

        private readonly Context _context;
        public AdminController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string pass, string? returnUrl)
        {
            if (username == "admin" && pass == "melisa")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Panel", "Admin");
            }

            ViewBag.LoginError = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult EventRegistrationsAdmin()
        {
            var today = DateTime.Today;
            var events = _context.Events
                .Where(e => e.Date >= today)
                .Select(e => new EventRegistrationsForAdminViewModel
                {
                    EventId = e.EventId,
                    EventTitle = e.Title,
                    EventDate = e.Date,
                    Registrations = _context.EventRegistrations
                        .Where(r => r.EventId == e.EventId)
                        .ToList()
                })
                .ToList();

            return View(events);
        }
    }
}
