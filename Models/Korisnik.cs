using System.ComponentModel.DataAnnotations;

namespace Filmovizija.Models
{
    public class Korisnik
    {
        [Key]
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public List<Posudba> Posudbe { get; set; } = new List<Posudba>();

    }
}
