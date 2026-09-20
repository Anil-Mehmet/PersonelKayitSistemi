using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PersonelKayitSistemi.Controllers
{
    public class LoginController : Controller
    {
        // Sabit tanımlı kullanıcı bilgileri
        private const string kullaniciEmail = "personel@personel.com";
        private const string kullaniciSifre = "personel123";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string sifre)
        {
            if (email == kullaniciEmail && sifre == kullaniciSifre)
            {
                // Giriş başarılı, session'da kullanıcı bilgisi tut
                HttpContext.Session.SetString("KullaniciEmail", email);

                // Ana sekmeli sayfaya yönlendir
                return RedirectToAction("Index", "Panel");
            }
            else
            {
                ViewBag.Hata = "E-posta veya şifre hatalı!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("KullaniciEmail");
            return RedirectToAction("Index");
        }
    }
}