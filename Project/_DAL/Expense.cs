
namespace DAL
{
    public class Expense : BaseEntity<Guid>
    {
        public decimal Amount { get; set; }
        public Card? Card { get; set; }
		public string? Currency { get; set; }
		public DateOnly Date { get; set; }
	}
}
