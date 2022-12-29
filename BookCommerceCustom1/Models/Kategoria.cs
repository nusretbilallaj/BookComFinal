using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCommerceCustom1.Models
{
    [Table("Kategoria")]
    public class Kategoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Specifikoni emrin")]
        public string Emri { get; set; }
        [Required(ErrorMessage = "Specifikoni renditjen")]
        [Range(1,100,ErrorMessage = "Specifikoni renditjen")]
        public int Renditja { get; set; }
    }
}
