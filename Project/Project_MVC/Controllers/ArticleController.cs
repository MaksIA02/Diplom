using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using Project_BLL;
using Project_MVC.Models;
using System.Xml.Linq;

namespace Project_MVC.Controllers
{
    public class ArticleController : Controller
    {
        readonly IKernel ninjectKernel = new StandardKernel(new IoC_BLL());

        public IActionResult Index()
        {
            var logic = ninjectKernel.Get<ILogic>();
            var Articles = logic.ShowArticle();
            return View(Articles);
        }
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Create(string Name, decimal Amount, string Currency)
        {
			var userName = User.Identity.Name;

			if (ModelState.IsValid)
            {
                var logic = ninjectKernel.Get<ILogic>();
                logic.AddArticle(Name, Amount, userName, Currency);

                return RedirectToAction("Index");
            }
            return View(Name);
        }

        public IActionResult Delete(string? name)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var Articles = logic.ShowArticle();
            foreach (var a in Articles)
            {
                if (a.Name == name)
                {
                    ArticleModel articleModel = new()
                    {
                        Name = a.Name,
                    };
                    return View(articleModel);
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
            logic.RemoveArticle(name);

            return RedirectToAction("Index");
        }

        public IActionResult Statistic()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Statistic(string? CardName, string? ArticleName)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var cards = logic.ShowCard();
            var articles = logic.ShowArticle();
            foreach (var c in cards)
            {
                foreach (var a in articles)
                {
                    if (ModelState.IsValid & c.Name == CardName & a.Name == ArticleName)
                    {
                        Statistic NewAllincome = new()
                        {
                            CardName = CardName,
                            ArticleName = ArticleName,
                            AllIncome = logic.ShowCardAndArticleStat(CardName, ArticleName),
                        };
                        return View(NewAllincome);
                    }
                }
            }
            return RedirectToAction("Error2");

        }
        public IActionResult UseArticle()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult UseArticle(UseArticle obj)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var cards = logic.ShowCard();
            var articles = logic.ShowArticle();
            foreach (var c in cards)
            {
                foreach (var a in articles)
                {
                    if (ModelState.IsValid & c.Name == obj.CardName & a.Name == obj.ArticleName)
                    {
                        logic.UseArticle(obj.CardName, obj.ArticleName);

                        if (obj.Reuse != null)
                        {
							logic.UseArticleAfterDays(obj.CardName, obj.ArticleName, obj.Reuse);
						}
                        return RedirectToAction("Cards", "Card");
                    }
                }
            }
            return RedirectToAction("Error");
        }

        public IActionResult ChangeAmount(string? name)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var articles = logic.ShowArticle();
            foreach (var a in articles)
            {
                if (a.Name == name)
                {
                    ArticleModel articleModel = new()
                    {
                        Name = a.Name,
                    };
                    return View(articleModel);
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
            logic.ChangeArticleAmount(name, amount);

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
		public IActionResult AllError()
		{
			return View();
		}

		public IActionResult SetLimit(string? name)
        {
            var logic = ninjectKernel.Get<ILogic>();
            var articles = logic.ShowArticle();
            foreach (var a in articles)
            {
                if (a.Name == name)
                {
                    ArticleModel articleModel = new()
                    {
                        Name = a.Name,

                    };

                    return View(articleModel);
                }
            }
            return NotFound();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult SetLimitPOST(string? name, decimal amount, string Date)
        {
            var logic = ninjectKernel.Get<ILogic>();
            // Перетворення рядка на DateOnly
            DateOnly date;
			if (ModelState.IsValid)
			{
			date = DateOnly.Parse(Date);  
            logic.SetLimit(name, amount, date);

            return RedirectToAction("Index");
			}
		    return RedirectToAction("AllError");
		}

		public IActionResult ArticleStatistic()
		{
			var logic = ninjectKernel.Get<ILogic>();

			var Article = logic.ShowArticle();
			return View(Article);
		}
	}
}
