namespace Buch.Models
{
    public class Kassenbuch
    {
        public int Id { get; set; }
        public double Anfangsbestand { get; set; }
        public string Monat { get; set; }
        public ICollection<Rechnung> Rechnungen { get; set;} = new List<Rechnung>();
    }
}
