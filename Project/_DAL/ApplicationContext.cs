using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_DAL;
using System.Reflection.Emit;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext 
    {      
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Transfer> Transfers { get; set; } = null!;
        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Debt> Debts { get; set; } = null!;
        public DbSet<Goal> Goals { get; set; } = null!;
		public DbSet<Receipt> Receipts { get; set; } = null!;



		public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Cards.db");
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Card>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<Article>()
                .HasIndex(c => c.Name)
                .IsUnique();
            base.OnModelCreating(builder);
        }
    }
}
