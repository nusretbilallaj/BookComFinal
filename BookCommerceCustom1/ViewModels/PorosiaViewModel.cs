using BookCommerceCustom1.Models;

namespace BookCommerceCustom1.ViewModels
{
	public class PorosiaViewModel
	{
		public Porosia Porosia { get; set; }
		public IEnumerable<PorosiDetali> PorosiDetalet { get; set; }
	}
}
