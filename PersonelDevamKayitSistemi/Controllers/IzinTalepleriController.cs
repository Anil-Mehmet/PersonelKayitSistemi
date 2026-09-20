using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelKayitSistemi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelKayitSistemi.Controllers
{
    public class IzinTalepleriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IzinTalepleriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IzinTalepleri
        // Hem muhasebe (personel) hem admin bu listeyi görür.
        public async Task<IActionResult> Index()
        {
            var talepler = await _context.IzinTalepleri
                .Include(t => t.Personel)
                .OrderByDescending(t => t.TalepTarihi)
                .ToListAsync();

            return View(talepler);
        }

        // GET: IzinTalepleri/Create
        // Sadece muhasebe elemanı (personel girişi) erişir.
        public IActionResult Create()
        {
            ViewBag.Personeller = _context.Personeller.OrderBy(p => p.AdSoyad).ToList();
            return View();
        }

        // POST: IzinTalepleri/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IzinTalebi izinTalebi)
        {
            // Personel navigation property'sini validasyondan çıkarıyoruz
            ModelState.Remove("Personel");

            if (izinTalebi.BitisTarihi < izinTalebi.BaslangicTarihi)
            {
                ModelState.AddModelError("BitisTarihi", "Bitiş tarihi başlangıç tarihinden önce olamaz.");
            }

            if (ModelState.IsValid)
            {
                izinTalebi.Durum = "Beklemede";
                izinTalebi.TalepTarihi = DateTime.Now;

                _context.IzinTalepleri.Add(izinTalebi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Personeller = _context.Personeller.OrderBy(p => p.AdSoyad).ToList();
            return View(izinTalebi);
        }

        // POST: IzinTalepleri/Onayla/5
        // Sadece admin (müdür) erişmeli.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Onayla(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminEmail")))
            {
                return RedirectToAction("Login", "Admin");
            }

            var talep = await _context.IzinTalepleri.FindAsync(id);
            if (talep != null)
            {
                talep.Durum = "Onaylandı";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: IzinTalepleri/Reddet/5
        // Sadece admin (müdür) erişmeli.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reddet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminEmail")))
            {
                return RedirectToAction("Login", "Admin");
            }

            var talep = await _context.IzinTalepleri.FindAsync(id);
            if (talep != null)
            {
                talep.Durum = "Reddedildi";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}