using Microsoft.AspNetCore.Mvc;
using PersonelKayitSistemi.Models;
using System.Linq; // LINQ metodları için gerekli
using System.Threading.Tasks; // Async/await için gerekli
using Microsoft.EntityFrameworkCore; // Async EF Core metodları için gerekli
using System; // DateTime için gerekli

namespace PersonelKayitSistemi.Controllers
{
    public class GirisCikisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GirisCikisController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Mesajın başlangıçta boş olmasını sağlıyoruz
            ViewBag.Mesaj = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Güvenlik için eklenmiştir
        public async Task<IActionResult> IslemYap(string barkodKodu) // Metot adı BarkodOku'dan IslemYap'a değiştirildi
        {
            // 1. Barkod Kodu Kontrolü
            if (string.IsNullOrWhiteSpace(barkodKodu))
            {
                ViewBag.Mesaj = "Lütfen bir barkod okutunuz.";
                return View("Index");
            }

            // 2. Personeli Barkod Koduna Göre Bulma (Asenkron olarak)
            var personel = await _context.Personeller.FirstOrDefaultAsync(p => p.BarkodNo == barkodKodu);

            // 3. Personel Bulunamadı Kontrolü
            if (personel == null)
            {
                ViewBag.Mesaj = $"Hata: '{barkodKodu}' barkod numaralı personel bulunamadı.";
                return View("Index");
            }

            // 4. Personelin Son Giriş Hareketini Bulma (Çıkış Tarihi Null Olan En Son Kayıt)
            // Bu sorgu, personelin şu anda içeride olup olmadığını (yani son girişinin çıkışının yapılıp yapılmadığını) kontrol eder.
            var sonGirisHareketi = await _context.Hareketler
                                            .Where(h => h.PersonelId == personel.Id && h.CikisTarihi == null)
                                            .OrderByDescending(h => h.GirisTarihi) // En son yapılan işlemi bulmak için
                                            .FirstOrDefaultAsync();

            string mesaj;

            if (sonGirisHareketi == null)
            {
                // 5. Durum A: Son bir giriş kaydı yoksa veya son giriş kaydının çıkışı yapılmışsa, yeni bir GİRİŞ kaydı oluştur.
                var yeniGiris = new Hareket
                {
                    PersonelId = personel.Id,
                    GirisTarihi = DateTime.Now,
                    CikisTarihi = null // Yeni bir giriş olduğu için çıkış tarihi boş bırakılır
                };
                _context.Hareketler.Add(yeniGiris);
                await _context.SaveChangesAsync(); // Asenkron olarak kaydet
                mesaj = $"{personel.AdSoyad} için GİRİŞ kaydedildi. ({yeniGiris.GirisTarihi.ToShortTimeString()})";
            }
            else
            {
                // 6. Durum B: Çıkış tarihi boş olan bir giriş kaydı varsa (yani personel içerideyse),
                //     bu giriş kaydının CikisTarihi'ni güncelleyerek ÇIKIŞ işlemini tamamla.
                sonGirisHareketi.CikisTarihi = DateTime.Now;
                _context.Hareketler.Update(sonGirisHareketi);
                await _context.SaveChangesAsync(); // Asenkron olarak kaydet
                mesaj = $"{personel.AdSoyad} için ÇIKIŞ kaydedildi. ({sonGirisHareketi.CikisTarihi.Value.ToShortTimeString()})";
            }

            ViewBag.Mesaj = mesaj;

            // İşlem sonrası aynı View'a geri dönüyoruz.
            return View("Index");
        }
    }
}