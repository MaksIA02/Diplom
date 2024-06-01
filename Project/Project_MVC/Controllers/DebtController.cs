using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using Project_BLL;
using Project_MVC.Models;

namespace Project_MVC.Controllers
{
    public class DebtController : Controller
    {
        readonly IKernel ninjectKernel = new StandardKernel(new IoC_BLL());

        public IActionResult Index()
        {
            var logic = ninjectKernel.Get<ILogic>();
            var Debts = logic.ShowDebt();
            return View(Debts);
        }
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Create(string? Name, string? debtor, decimal Amount, string Currency)
        {
			var userName = User.Identity.Name;

			if (ModelState.IsValid)
            {
                var logic = ninjectKernel.Get<ILogic>();
                logic.AddDebt(Name, userName, debtor, Amount, Currency);

                return RedirectToAction("Index");
            }
            return View(Name);
        }

        public IActionResult Delete(string? name)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var Debts = logic.ShowDebt();
            foreach (var d in Debts)
            {
                if (d.Name == name)
                {
                    DebtModel debtModel = new()
                    {
                        Name = d.Name,
                    };
                    return View(debtModel);
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
            logic.RemoveDebt(name);

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Error2()
        {
            return View();
        }

		public IActionResult DebtStatistic()
		{
			var logic = ninjectKernel.Get<ILogic>();

			var Debt = logic.ShowDebt();
			return View(Debt);
		}
	}
}
