namespace DAL
{
    public class Card : BaseEntity<Guid>
    { 
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public decimal? Goal { get; set; }
        public DateOnly? GoalDate { get; set; }
        public List<Income> Incomes { get; set; } = new();
        public List<Expense> Expenses { get; set; } = new();
        public List<Transfer> Transfers { get; set; } = new();
		
	}
}