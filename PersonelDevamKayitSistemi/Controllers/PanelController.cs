using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelKayitSistemi.Models;
using System;
using System.Linq;

namespace PersonelKayitSistemi.Controllers
{
    public class PanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bugun = DateTime.Today;

            var model = new PanelViewModel
            {
                ToplamPersonel = _context.Personeller.Count(),

                BugunGiris = _context.Hareketler
                    .Count(h => h.GirisTarihi.Date == bugun),

                BugunCikis = _context.Hareketler
                    .Count(h => h.CikisTarihi.HasValue && h.CikisTarihi.Value.Date == bugun),

                SuAnIcerideOlan = _context.Hareketler
                    .Count(h => h.CikisTarihi == null),

                SonHareketler = _context.Hareketler
                    .Include(h => h.Personel)
                    .OrderByDescending(h => h.CikisTarihi ?? h.GirisTarihi)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }
    }
}