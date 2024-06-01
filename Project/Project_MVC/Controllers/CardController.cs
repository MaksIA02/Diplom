using Project_BLL;
using Microsoft.AspNetCore.Mvc;
using Project_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Ninject;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Project_MVC.Controllers
{
	public class CardController : Controller
	{
        readonly IKernel ninjectKernel = new StandardKernel(new IoC_BLL());

        public IActionResult Cards()
		{       
            var logic = ninjectKernel.Get<ILogic>();
          
			var Cards = logic.ShowCard();
			return View(Cards);
		}

		public IActionResult CardsPDF()
		{
			var logic = ninjectKernel.Get<ILogic>();

			var Cards = logic.ShowCard();
			return View(Cards);
		}

		public IActionResult Create()
		{
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Create(string Name, string Currency)
		{
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
			var userName = User.Identity.Name;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

			if (ModelState.IsValid)
			{
                var logic = ninjectKernel.Get<ILogic>();
                logic.AddCard(Name, userName, Currency);

				return RedirectToAction("Cards");
			}
			return View(Name);
		}

		public IActionResult ChangeAmount(string? name)
		{
            var logic = ninjectKernel.Get<ILogic>();
            var cards = logic.ShowCard();
			foreach (var c in cards)
			{
				if (c.Name == name)
				{
                    CardModel cardModel = new ()
                    {
                        Name = c.Name,
           
                    };
                    
					return View(cardModel);
				}
			}
			return NotFound();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult ChangeAmountPOST(string? name, decimal amount)
		{
            var logic = ninjectKernel.Get<ILogic>();
            logic.MakeTransfer(name, amount);

			return RedirectToAction("Cards");
		}

		public IActionResult Delete(string? name)
		{
            var logic = ninjectKernel.Get<ILogic>();
            var cards = logic.ShowCard();
            foreach (var c in cards)
            {
                if (c.Name == name)
                {
                    CardModel cardModel = new()
                    {
                        Name = c.Name,
                    };
                    return View(cardModel);
                }
            }
            return NotFound();
        }

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult DeletePOST(string? name)
		{
			var logic = ninjectKernel.Get<ILogic>();
			logic.RemoveCard(name);

			return RedirectToAction("Cards");
		}

		public IActionResult Statistic(string? name)
		{	
			var logic = ninjectKernel.Get<ILogic>();
			var cards = logic.ShowCard();


			Statistic NewAllincome = new()
			{
				CardName = name,
				AllIncome = logic.ShowCardIncome(name),
				AllExpense = logic.ShowCardExpense(name),

			};

			return View(NewAllincome);
		}

		public IActionResult Exchange()
		{
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Exchange(ExchangeTransfer obj)
		{
			var logic = ninjectKernel.Get<ILogic>();
			var cards = logic.ShowCard();
			foreach (var c in cards)
			{
				foreach (var j in cards)
				{
					if (ModelState.IsValid && c.Name == obj.CardName1 && j.Name == obj.CardName2)
					{
						logic.MakeTransferFor2Crads(obj.CardName1, obj.CardName2, obj.Amount);

						return RedirectToAction("Cards");
					}
				}
			}
			return RedirectToAction("Error");
		}

		public IActionResult Error()
		{
			return View();
		}
		public IActionResult AllError()
		{
			return View();
		}

		public IActionResult SetGoal(string? name)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var cards = logic.ShowCard();
            foreach (var c in cards)
            {
                if (c.Name == name)
                {
                    CardModel cardModel = new()
                    {
                        Name = c.Name,

                    };

                    return View(cardModel);
                }
            }
            return NotFound();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult SetGoalPOST(string? name, decimal amount, string Date)
        {
            var logic = ninjectKernel.Get<ILogic>();
			// Перетворення рядка на DateOnly
			if (ModelState.IsValid)
			{
				DateOnly date = DateOnly.Parse(Date);
				logic.SetGoal(name, amount, date);

				return RedirectToAction("Cards");
			}
			return RedirectToAction("AllError");
		}

		public IActionResult GraphStatistic()
		{
			var logic = ninjectKernel.Get<ILogic>();

			var Incomes = logic.ShowIncome();
			return View(Incomes);
		}

		public IActionResult ExpenseStatistic()
		{
			var logic = ninjectKernel.Get<ILogic>();

			var Expense = logic.ShowExpense();
			return View(Expense);
		}
	}
}
