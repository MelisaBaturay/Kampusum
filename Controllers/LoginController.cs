using Kampusum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Kampusum.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!user.UserEmail.EndsWith("edu.tr", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "Sadece .edu.tr uzantılı e-posta adresleri kabul edilir.";
                return RedirectToAction("Login");
            }

            var checkUser = _context.Users.FirstOrDefault(u => u.UserEmail == user.UserEmail);
            if (checkUser != null)
            {
                TempData["ErrorMessage"] = "Bu e-posta ile kayıtlı bir kullanıcı zaten var.";
                return RedirectToAction("Login");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password, string? returnUrl)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email && u.UserPassword == password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "E-posta veya şifre hatalı!";
                return RedirectToAction("Login");
            }
            if (user != null)
            {
                var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, user.UserName),
    new Claim(ClaimTypes.Email, user.UserEmail)
};

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Starter", "Home");
            }
            else
            {
                ViewBag.LoginError = "E-posta veya şifre hatalı!";
                return View("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Starter", "Home");
        }
    }
}
