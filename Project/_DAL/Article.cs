namespace DAL
{
    public class Article : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public decimal? LimitAmount { get; set; }
		public string? Currency { get; set; }
		public int? Reuse { get; set; }
		public DateOnly? LimitDate { get; set; }
        public List<Transfer> Transfers { get; set; } = new();
    }
}
