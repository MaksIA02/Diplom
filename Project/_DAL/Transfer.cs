namespace DAL
{
    public class Transfer : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public Card? Card { get; set; }
        public Article? Article { get; set; }
    }
}
