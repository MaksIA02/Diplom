using DAL;
using Ninject;
using Project_DAL;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace Project_BLL
{
	public class BLL : ILogic
	{
		readonly IKernel ninjectKernel = new StandardKernel(new IoC_DAL());

		public void AddCard(string? Name, string? UserName, string? Currency)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();


			Card NewCard = new() { Name = Name, Amount = 0, IdentityUser = UserName, Currency = Currency };

			try
			{

				DB.Cards.Create(NewCard);
				DB.Save();
			}
			catch
			{

			}
		}

		public void RemoveCard(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();
			var cards = DB.Cards.GetWithInclude(c => c.Transfers, c => c.Incomes, c => c.Expenses);

			foreach (Card c in cards)
			{
				if (c.Name == Name)
				{
					DB.Cards.Delete(c);

				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		public IEnumerable<Card> ShowCard()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var cards = DB.Cards.GetAll();

			return cards;
		}

		public void MakeTransfer(string? CardName, decimal Amount)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();



			var cards = DB.Cards.GetWithInclude(c => c.Transfers, c => c.Incomes, c => c.Expenses);


			foreach (Card c in cards)
			{
				if (c.Name == CardName)
				{
					Transfer NewTransfer = new() { Amount = Amount, Card = c };
					DB.Transfers.Create(NewTransfer);

					c.Transfers.Add(NewTransfer);
					c.Amount += Amount;

					if (Amount >= 0)
					{
						Income NewIncome = new() { Amount = Amount, Date = DateOnly.FromDateTime(DateTime.Now), Currency = c.Currency };
						c.Incomes.Add(NewIncome);
					}
					if (Amount < 0)
					{
						Expense NewExpense = new() { Amount = Amount, Date = DateOnly.FromDateTime(DateTime.Now), Currency = c.Currency };
						c.Expenses.Add(NewExpense);
					}
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		public void MakeTransferFor2Crads(string? CardName, string? CardName2, decimal Amount)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var cards = DB.Cards.GetWithInclude(c => c.Transfers, c => c.Incomes, c => c.Expenses); ;

			foreach (Card c in cards)
			{
				if (c.Name == CardName)
				{
					foreach (Card i in cards)
					{
						if (i.Name == CardName2)
						{
							Transfer NewTransfer1 = new() { Amount = -Amount, Card = c };
							DB.Transfers.Create(NewTransfer1);

							Transfer NewTransfer2 = new() { Amount = Amount, Card = i };
							DB.Transfers.Create(NewTransfer2);

							c.Transfers.Add(NewTransfer1);
							i.Transfers.Add(NewTransfer2);

							c.Amount -= Amount;
							i.Amount += Amount;

							Income NewIncome = new() { Amount = Amount, Card = i, Date = DateOnly.FromDateTime(DateTime.Now), Currency = i.Currency };
							i.Incomes.Add(NewIncome);

							Expense NewExpense = new() { Amount = -Amount, Card = c, Date = DateOnly.FromDateTime(DateTime.Now), Currency = c.Currency };
							c.Expenses.Add(NewExpense);
						}
					}
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		public void SetGoal(string? Name, decimal Amount, DateOnly Date)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var cards = DB.Cards.GetWithInclude();

			foreach (Card c in cards)
			{
				if (c.Name == Name)
				{
					c.Goal = Amount;
					c.GoalDate = Date;
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		// Статті

		public void AddArticle(string? Name, decimal Amount, string? UserName, string? Currency)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			Article NewArticle = new() { Name = Name, Amount = Amount, IdentityUser = UserName, Currency = Currency };
			try
			{
				DB.Articles.Create(NewArticle);
				DB.Save();
			}
			catch
			{

			}
		}

		public void UseArticle(string? CardName, string? ArticleName)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var cards = DB.Cards.GetWithInclude(c => c.Transfers, c => c.Incomes, c => c.Expenses);
			var articles = DB.Articles.GetWithInclude(a => a.Transfers);


			foreach (Card c in cards)
			{
				if (c.Name == CardName)
				{
					foreach (Article a in articles)
					{
						if (a.Name == ArticleName)
						{
							Transfer NewTransfer = new() { Name = a.Name, Amount = a.Amount, Card = c, Article = a };
							DB.Transfers.Create(NewTransfer);
							a.Transfers.Add(NewTransfer);
							c.Transfers.Add(NewTransfer);

							if (c.Currency == "₴ UAH")
								switch (a.Currency)
								{
									case "₴ UAH":
										c.Amount += NewTransfer.Amount;
										break;
									case "$ USD":
										c.Amount += NewTransfer.Amount*40;
										break;
									case "€ EUR":
										c.Amount += NewTransfer.Amount*42;
										break;
									case "₿ BTC":
										c.Amount += NewTransfer.Amount*2377904;
										break;
								}
							if (c.Currency == "$ USD")
								switch (a.Currency)
								{
									case "₴ UAH":
										c.Amount += NewTransfer.Amount/40;
										break;
									case "$ USD":
										c.Amount += NewTransfer.Amount;
										break;
									case "€ EUR":
										c.Amount += NewTransfer.Amount;
										break;
									case "₿ BTC":
										c.Amount += NewTransfer.Amount * 60000;
										break;
								}
							if (c.Currency == "€ EUR")
								switch (a.Currency)
								{
									case "₴ UAH":
										c.Amount += NewTransfer.Amount / 42;
										break;
									case "$ USD":
										c.Amount += NewTransfer.Amount;
										break;
									case "€ EUR":
										c.Amount += NewTransfer.Amount;
										break;
									case "₿ BTC":
										c.Amount += NewTransfer.Amount * 57000;
										break;
								}
							if (c.Currency == "₿ BTC")
								switch (a.Currency)
								{
									case "₴ UAH":
										c.Amount += NewTransfer.Amount / 2377904;
										break;
									case "$ USD":
										c.Amount += NewTransfer.Amount / 60000;
										break;
									case "€ EUR":
										c.Amount += NewTransfer.Amount / 57000;
										break;
									case "₿ BTC":
										c.Amount += NewTransfer.Amount;
										break;
								}

							if (a.Amount >= 0)
							{
								Income NewIncome = new() { Amount = a.Amount, Card = c, Date = DateOnly.FromDateTime(DateTime.Now), Currency = c.Currency };
								c.Incomes.Add(NewIncome);
							}
							if (a.Amount < 0)
							{
								Expense NewExpense = new() { Amount = a.Amount, Card = c, Date = DateOnly.FromDateTime(DateTime.Now), Currency = c.Currency };
								c.Expenses.Add(NewExpense);
							}
						}
					}
				}
			}
			DB.Save();
		}

		public IEnumerable<Article> ShowArticle()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var articles = DB.Articles.GetAll();

			return articles;
		}

		public IEnumerable<Income> ShowIncome()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var incomes = DB.Incomes.GetAll();

			return incomes;
		}

		public IEnumerable<Expense> ShowExpense()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var expense = DB.Expenses.GetAll();

			return expense;
		}

		public IEnumerable<Debt> ShowDebts()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var debt = DB.Debts.GetAll();

			return debt;
		}

		public void RemoveArticle(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var articles = DB.Articles.GetWithInclude(a => a.Transfers);

			foreach (Article a in articles)
			{
				if (a.Name == Name)
				{
					DB.Articles.Delete(a);
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		public decimal ShowCardIncome(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var cards = DB.Cards.GetWithInclude(c => c.Transfers, c => c.Incomes, c => c.Expenses);
			decimal AllIncome = 0;

			foreach (Card c in cards)
			{
				if (c.Name == Name)
				{
					foreach (Income i in c.Incomes)
					{
						AllIncome += i.Amount;
					}
				}
			}
			return AllIncome;
		}

		public decimal ShowCardAndArticleStat(string? CardName, string? ArticleName)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();
			decimal AllIncomeInArticle = 0;

			var cards = DB.Cards.GetWithInclude(c => c.Transfers, c => c.Incomes, c => c.Expenses);
			var articles = DB.Articles.GetWithInclude(a => a.Transfers);

			foreach (Card c in cards)
			{
				if (c.Name == CardName)
				{
					foreach (Article a in articles)
					{
						if (a.Name == ArticleName)
						{
							var transfers = c.Transfers;
							foreach (Transfer t in transfers)
							{
								if (t.Name == a.Name)
								{
									AllIncomeInArticle += t.Amount;
								}
							}
						}
					}
				}
			}
			return AllIncomeInArticle;
		}

		public decimal ShowCardExpense(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var cards = DB.Cards.GetWithInclude(c => c.Transfers, c => c.Incomes, c => c.Expenses);
			decimal AllExpense = 0;

			foreach (Card c in cards)
			{
				if (c.Name == Name)
				{
					foreach (Expense e in c.Expenses)
					{
						AllExpense += e.Amount;
					}
				}
			}
			return AllExpense;
		}

		public IEnumerable<Transfer> ShowTransfer()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var transfers = DB.Transfers.GetAll();

			return transfers;
		}

		public void ChangeArticleAmount(string? Name, decimal Amount)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var articles = DB.Articles.GetWithInclude(a => a.Transfers);

			foreach (Article a in articles)
			{
				if (a.Name == Name)
				{
					a.Amount = Amount;
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		public void SetLimit(string? Name, decimal Amount, DateOnly Date)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var articles = DB.Articles.GetWithInclude();

			foreach (Article a in articles)
			{
				if (a.Name == Name)
				{
					a.LimitAmount = Amount;
					a.LimitDate = Date;
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		// Борги

		public void AddDebt(string? Name, string? UserName, string? debtor, decimal Amount, string? Currency)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();


			Debt NewDebt = new() { Name = Name, Amount = Amount, IdentityUser = UserName, Debtor = debtor, Date = DateOnly.FromDateTime(DateTime.Now), Currency = Currency };

			try
			{

				DB.Debts.Create(NewDebt);
				DB.Save();
			}
			catch
			{

			}
		}

		public void RemoveDebt(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();
			var debts = DB.Debts.GetWithInclude();

			foreach (Debt d in debts)
			{
				if (d.Name == Name)
				{
					DB.Debts.Delete(d);
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}

		public IEnumerable<Debt> ShowDebt()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var debts = DB.Debts.GetAll();

			return debts;
		}

		public void UseArticleAfterDays(string? CardName, string? ArticleName, int? daysToAdd)
		{
			Task.Delay(TimeSpan.FromDays((double)daysToAdd)).ContinueWith(_ =>
			{
				UseArticle(CardName, ArticleName);
			});
		}

		// чеки 

		public void AddReceipt(string? Name, string? UserName, string? description, decimal Amount, string? Currency, byte[]? photo)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();


			Receipt NewReceipt = new() { Name = Name, Amount = Amount, IdentityUser = UserName, Description = description, Date = DateOnly.FromDateTime(DateTime.Now), Currency = Currency,
			Photo = photo};

			try
			{

				DB.Receipts.Create(NewReceipt);
				DB.Save();
			}
			catch
			{

			}
		}
		public void RemoveReceipt(string? Name)
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();
			var receipts = DB.Receipts.GetWithInclude();

			foreach (Receipt r in receipts)
			{
				if (r.Name == Name)
				{
					DB.Receipts.Delete(r);
				}
			}
			try
			{
				DB.Save();
			}
			catch
			{

			}
		}
		public IEnumerable<Receipt> ShowReceipt()
		{
			var DB = ninjectKernel.Get<IUnitOfWork>();

			var receipts = DB.Receipts.GetAll();

			return receipts;
		}

	}
}