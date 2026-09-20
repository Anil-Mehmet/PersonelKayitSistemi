using System;
using System.ComponentModel.DataAnnotations;

namespace PersonelKayitSistemi.Models
{
    public class Hareket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonelId { get; set; }

        [Required]
        public DateTime GirisTarihi { get; set; } = DateTime.Now;

        public DateTime? CikisTarihi { get; set; }

        // Navigation property (ilişki)
        public Personel Personel { get; set; }
    }
}