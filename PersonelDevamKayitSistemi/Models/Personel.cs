using System.ComponentModel.DataAnnotations;

namespace PersonelKayitSistemi.Models
{
    public class Personel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string AdSoyad { get; set; }

        [Required]
        [StringLength(11)]
        public string TCKimlikNo { get; set; }

        [StringLength(20)]
        public string BarkodNo { get; set; }

        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}
