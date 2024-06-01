using System.ComponentModel.DataAnnotations;

namespace Project_MVC.Models
{
	public class UseArticle
	{
		[Required]
		public string? CardName { get; set; }
		[Required]
		public string? ArticleName { get; set; }

		public int? Reuse { get; set; }
	}
}
