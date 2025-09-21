using System.ComponentModel.DataAnnotations;

namespace Filmovizija.Models
{
    public class Film
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Zanr { get; set; }
        public int GodinaIzdanja { get; set; }
        public string Reziser { get; set; }
        public double Ocjena { get; set; }

        public List<Posudba> Posudbe { get; set; } = new List<Posudba>();
    }
}
