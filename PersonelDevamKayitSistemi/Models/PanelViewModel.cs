using System.Collections.Generic;

namespace PersonelKayitSistemi.Models
{
    public class PanelViewModel
    {
        public int ToplamPersonel { get; set; }
        public int BugunGiris { get; set; }
        public int BugunCikis { get; set; }
        public int SuAnIcerideOlan { get; set; }

        public List<Hareket> SonHareketler { get; set; } = new List<Hareket>();
    }
}