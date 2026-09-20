using System;
using System.ComponentModel.DataAnnotations;

namespace PersonelKayitSistemi.Models
{
    public class IzinTalebi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonelId { get; set; }

        // Navigation property
        public Personel Personel { get; set; }

        [Required]
        [StringLength(50)]
        public string IzinTuru { get; set; }

        [Required]
        public DateTime BaslangicTarihi { get; set; }

        [Required]
        public DateTime BitisTarihi { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        [Required]
        [StringLength(20)]
        public string Durum { get; set; } = "Beklemede";

        public DateTime TalepTarihi { get; set; } = DateTime.Now;
    }
}