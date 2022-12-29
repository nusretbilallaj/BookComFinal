using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCommerceCustom1.Models
{
    public class Perdorusi : IdentityUser
    {
        [Required]
        public string Emri { get; set; }
        public string? Rruga { get; set; }
        public string? Qyteti { get; set; }
        public string? Shteti { get; set; }
        public string? KodiPostal { get; set; }
        public int? KompaniaId { get; set; }
        [ForeignKey("KompaniaId")]
        [ValidateNever]
        public Kompania Kompania { get; set; }
    }
}
