
namespace DAL
{
    public class Receipt : BaseEntity<Guid>
    {
		public string? Name { get; set; }
		public decimal Amount { get; set; }
		public string? Currency { get; set; }
		public string? Description { get; set; }
		public DateOnly Date { get; set; }
		public byte[]? Photo { get; set; }
	}
}
