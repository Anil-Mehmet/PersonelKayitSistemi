using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelKayitSistemi.Models;

namespace PersonelKayitSistemi.Controllers
{
    public class HareketlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HareketlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string barkodNo, string baslangic, string bitis)
        {
            var hareketler = _context.Hareketler
                .Include(h => h.Personel)
                .AsQueryable();

            if (!string.IsNullOrEmpty(barkodNo))
            {
                hareketler = hareketler.Where(h => h.Personel.BarkodNo == barkodNo);
            }

            if (!string.IsNullOrEmpty(baslangic) && DateTime.TryParse(baslangic, out DateTime baslangicTarih))
            {
                hareketler = hareketler.Where(h => h.GirisTarihi >= baslangicTarih);
            }

            if (!string.IsNullOrEmpty(bitis) && DateTime.TryParse(bitis, out DateTime bitisTarih))
            {
                hareketler = hareketler.Where(h => h.GirisTarihi <= bitisTarih);
            }

            // ViewBag'ler tekrar dolduruluyor
            ViewBag.BarkodNo = barkodNo;
            ViewBag.Baslangic = baslangic;
            ViewBag.Bitis = bitis;

            var sonuc = hareketler
                .OrderByDescending(h => h.CikisTarihi ?? h.GirisTarihi)
                .ToList();

            return View(sonuc);
        }
    }
}
