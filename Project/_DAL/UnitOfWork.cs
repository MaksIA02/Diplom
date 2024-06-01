using Microsoft.AspNetCore.Identity;
using Project_DAL;
using Project_DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
		private readonly ApplicationContext db = new();

        private ICardRepository? cardGenRepository;
        private IArticleRepository? articleGenRepository;
        private ITransferRepository? transferGenRepository;
        private IIncomeRepository? incomeGenRepository;
        private IExpenseRepository? expenseGenRepository;
        private IDebtRepository? debtGenRepository;
        private IGoalRepository? goalGenRepository;
		private IReceiptRepository? receiptGenRepository;

		public ICardRepository Cards
        {
            get
            {
                cardGenRepository ??= new CardRepository(db);
                return cardGenRepository;
            }
        }

        public IArticleRepository Articles
        {
            get
            {
                articleGenRepository ??= new ArticleRepository(db);
                return articleGenRepository;
            }
        }

        public ITransferRepository Transfers
        {
            get
            {
                transferGenRepository ??= new TransferRepository(db);
                return transferGenRepository;
            }
        }

        public IIncomeRepository Incomes
        {
            get
            {
                incomeGenRepository ??= new IncomeRepository(db);
                return incomeGenRepository;
            }
        }

        public IExpenseRepository Expenses
        {
            get
            {
                expenseGenRepository ??= new ExpenseRepository(db);
                return expenseGenRepository;
            }
        }
		public IDebtRepository Debts
		{
			get
			{
				debtGenRepository ??= new DebtRepository(db);
				return debtGenRepository;
			}
		}
        public IGoalRepository Goals
        {
            get
            {
                goalGenRepository ??= new GoalRepository(db);
                return goalGenRepository;
            }
        }
		public IReceiptRepository Receipts
		{
			get
			{
				receiptGenRepository ??= new ReceiptRepository(db);
				return receiptGenRepository;
			}
		}

		public void Save()
        {
            db.SaveChanges();
        }

		private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
