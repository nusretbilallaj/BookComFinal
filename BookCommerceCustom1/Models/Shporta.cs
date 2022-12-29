using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCommerceCustom1.Models
{
    [Table("Shporta")]
	public class Shporta
	{
        public int Id { get; set; }
        public int ProduktiId { get; set; }
        [ForeignKey("ProduktiId")]
        [ValidateNever]
        public Produkti Produkti { get; set; }
        [Range(1, 1000, ErrorMessage = "Please sasia 1 dhe 1000")]
        public int Sasia { get; set; }
        public string PerdorusiId { get; set; }
        [ForeignKey("PerdorusiId")]
        public Perdorusi Perdorusi { get; set; }

        [NotMapped]
        public double Cmimi { get; set; }

    }
}
