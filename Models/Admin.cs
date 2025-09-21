using System.ComponentModel.DataAnnotations;

namespace Filmovizija.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Lozinka { get; set; }
    }
}
