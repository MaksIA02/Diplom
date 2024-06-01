using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Models
{
    public class ReceiptModel
    {
        [Required]
        public string? Name { get; set; }
		public string? Description { get; set; }
		public decimal Amount { get; set; }
		public string? Currency { get; set; }
		public IFormFile? Photo { get; set; }
	}
}