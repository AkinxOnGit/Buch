using System.ComponentModel.DataAnnotations.Schema;

namespace Buch.Models
{
    public class Rechnung
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public double Betrag { get; set; }
        public string Art { get; set; }
        public ICollection<RechnungKategorie> RechnungKategories { get; set; } = new List<RechnungKategorie>();
        [ForeignKey("Kassenbuch")]
        public int KassenbuchId { get; set; } 
        public Kassenbuch Kassenbuch { get; set; }

    }
}
