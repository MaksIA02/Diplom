using DAL;
using Project_DAL;

namespace Project_BLL
{
    public interface ILogic
    {
        public void AddCard(string? Name, string? UserName, string? Currency);

		public IEnumerable<Card> ShowCard();

        void RemoveCard(string? Name);

        void MakeTransfer(string? CardName, decimal Amount);

        void MakeTransferFor2Crads(string? CardName, string? CardName2, decimal Amount);

        public void AddArticle(string? Name, decimal Amount, string? UserName, string? Currency);

		void UseArticle (string? CardName, string? ArticleName);

        public IEnumerable<Article> ShowArticle();
        public IEnumerable<Income> ShowIncome();
        public IEnumerable<Expense> ShowExpense();

        public IEnumerable<Debt> ShowDebts();

		void RemoveArticle(string? Name);

        decimal ShowCardIncome(string? Name);

        decimal ShowCardExpense(string? Name);

        decimal ShowCardAndArticleStat(string? CardName, string? ArticleName);

        public IEnumerable<Transfer> ShowTransfer();

        public void ChangeArticleAmount(string? Name, decimal Amount);

		//

		public void AddDebt(string? Name, string? UserName, string? debtor, decimal Amount, string? Currency);

		void RemoveDebt(string? Name);

		public IEnumerable<Debt> ShowDebt();

        //

        public void SetGoal(string? Name, decimal Amount, DateOnly Date);

        public void SetLimit(string? Name, decimal Amount, DateOnly Date);

        public void UseArticleAfterDays(string? CardName, string? ArticleName, int? daysToAdd);

        public void AddReceipt(string? Name, string? UserName, string? description, decimal Amount, string? Currency, byte[]? photo);
        public void RemoveReceipt(string? Name);
        public IEnumerable<Receipt> ShowReceipt();
	}
}