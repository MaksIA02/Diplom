using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Models
{
	public class ExchangeTransfer
	{
		[Required]
		public string? CardName1 { get; set; }
		[Required]
		public string? CardName2 { get; set; }
		[Required]
		public decimal Amount { get; set; }
	}
}
