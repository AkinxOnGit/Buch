namespace Buch.Models
{
    public class Kategorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Farbe { get; set; }

        public ICollection<RechnungKategorie> RechnungKategories { get; set; } = new List<RechnungKategorie>();
    }
}
