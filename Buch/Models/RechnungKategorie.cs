using System.ComponentModel.DataAnnotations.Schema;

namespace Buch.Models
{
    public class RechnungKategorie
    {
        public int Id { get; set; }

        [ForeignKey("Kategorie")]
        public int KategorieId { get; set; }
        public Kategorie Kategorie { get; set; }

        [ForeignKey("Rechnung")]
        public int RechnungId { get; set; }

        public Rechnung Rechnung { get; set; }

    }
}
