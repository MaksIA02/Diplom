using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Models
{
    public class DebtModel
    {
        [Required]
        public string? Name { get; set; }
		public string? Debtor { get; set; }
		public decimal Amount { get; set; }
		public string? Currency { get; set; }
	}
}