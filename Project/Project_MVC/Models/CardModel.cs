using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Models
{
    public class CardModel
    {
        [Required]
        public string? Name { get; set; }
        public decimal Amount { get; set; }
		[Required]
		public string? Date { get; set; }
        public string? Currency { get; set; }
    }
}