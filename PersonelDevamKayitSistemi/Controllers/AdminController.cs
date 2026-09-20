using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PersonelKayitSistemi.Controllers
{
    public class AdminController : Controller
    {
        // Sabit admin bilgileri
        private const string adminEmail = "admin@admin.com";
        private const string adminSifre = "admin123";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            if (email == adminEmail && sifre == adminSifre)
            {
                // Session'a admin bilgisi yazılır
                HttpContext.Session.SetString("AdminEmail", email);
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                ViewBag.Hata = "Admin e-posta veya şifre hatalı!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminEmail");
            return RedirectToAction("Login");
        }
    }
}
