using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCommerceCustom1.Models
{
    [Table("Kompania")]
    public class Kompania
    {
        public int Id { get; set; }
        [Required]
        public string Emri { get; set; }
        public string? Rruga { get; set; }
        public string? Qyteti { get; set; }
        public string? Shteti { get; set; }
        public string? KodiPostal { get; set; }
        public string? NumriTelefonit { get; set; }
    }
}
