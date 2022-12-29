using BookCommerceCustom1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCommerceCustom1.ViewModels
{
	public class ProduktiViewModel
	{
        public Produkti Produkti { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Kategorite { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Mbulesat { get; set; }
	}
}
