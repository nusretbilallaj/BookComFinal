using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;

namespace BookCommerceCustom1.Models
{
	[Table("Porosia")]
	public class Porosia
	{
		public int Id { get; set; }
		public string PerdorusiId { get; set; }
		[ForeignKey("PerdorusiId")]
		[ValidateNever]
		public Perdorusi Perdorusi { get; set; }

		public DateTime DataEPorosise { get; set; }
		public DateTime DataEDergeses { get; set; }
		public double Totali { get; set; }
		public string? StatusiIPorosise { get; set; }
		public string? StatusiIPageses { get; set; }

		public string? Posta { get; set; }
		public string? NumriGjurmues { get; set; }

		public DateTime DataEPageses { get; set; }
		public DateTime DataPerPagese { get; set; }

		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }
		[Required]
		public string NumriITelefonit { get; set; }
		[Required]
		public string Rruga { get; set; }
		[Required]
		public string Qyteti { get; set; }
		[Required]
		public string Shteti { get; set; }
		[Required]
		public string KodiPostal { get; set; }
		[Required]
		public string Emri { get; set; }
	}
}
