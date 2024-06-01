using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using Project_BLL;
using Project_MVC.Models;

namespace Project_MVC.Controllers
{
	public class ReceiptController : Controller
	{
		readonly IKernel ninjectKernel = new StandardKernel(new IoC_BLL());

		public IActionResult Index()
		{
			var logic = ninjectKernel.Get<ILogic>();
			var Receipts = logic.ShowReceipt();
			return View(Receipts);
		}
		public IActionResult Create()
		{
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> CreateAsync(string? Name, string? description, decimal Amount, string Currency, IFormFile? photo)
		{
			var userName = User.Identity.Name;

			if (ModelState.IsValid)
			{

				var logic = ninjectKernel.Get<ILogic>();

				byte[] photoData = null;

				if (photo != null && photo.Length > 0)
				{
					using (var ms = new MemoryStream())
					{
						await photo.CopyToAsync(ms);
						photoData = ms.ToArray();
					}
				}

				logic.AddReceipt(Name, userName, description, Amount, Currency, photoData);

				return RedirectToAction("Index");
			}
			return View(Name);
		}

		public IActionResult Delete(string? name)
		{
			var logic = ninjectKernel.Get<ILogic>();
			var Receipts = logic.ShowReceipt();
			foreach (var r in Receipts)
			{
				if (r.Name == name)
				{
					ReceiptModel receiptModel = new()
					{
						Name = r.Name,
					};
					return View(receiptModel);
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
			logic.RemoveReceipt(name);

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

		public IActionResult ReceiptStatistic()
		{
			var logic = ninjectKernel.Get<ILogic>();

			var Debt = logic.ShowReceipt();
			return View(Debt);
		}
	}
}
