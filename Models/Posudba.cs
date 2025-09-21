using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmovizija.Models
{
    public class Posudba
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KorisnikId { get; set; }
        [ForeignKey("KorisnikId")]
        public Korisnik Korisnik { get; set; }

        [Required]
        public int FilmId { get; set; }
        [ForeignKey("FilmId")]
        public Film Film { get; set; }

        [Required]
        public DateTime DatumPosudbe { get; set; }

        public DateTime? DatumPovratka { get; set; }

        public bool Vraceno { get; set; } = false;
    }
}
