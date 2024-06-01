using Project_DAL.Repositories;
using DAL;
using Microsoft.AspNetCore.Identity;

namespace Project_DAL
{
    public interface IUnitOfWork
    {
        ICardRepository Cards { get; }
        IArticleRepository Articles { get; }
        ITransferRepository Transfers { get; }
        IIncomeRepository Incomes { get; }
        IExpenseRepository Expenses { get; }
        IDebtRepository Debts { get; }
        IGoalRepository Goals { get; }
		IReceiptRepository Receipts { get; }
		public void Save();

	}
}
