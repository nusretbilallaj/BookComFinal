using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;

namespace BookCommerceCustom1.Models
{
	public class PorosiDetali
	{
		public int Id { get; set; }
		[Required]
		public int PorosiaId { get; set; }
		[ForeignKey("PorosiaId")]
		[ValidateNever]
		public Porosia Porosia { get; set; }

		[Required]
		public int ProduktiId { get; set; }
		[ForeignKey("ProduktiId")]
		[ValidateNever]
		public Produkti Produkti { get; set; }
		public double Sasia { get; set; }
		public double Cmimi { get; set; }
	}
}
