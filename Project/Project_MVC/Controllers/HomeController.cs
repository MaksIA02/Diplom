using Microsoft.AspNetCore.Mvc;
using Project_BLL;
using Project_MVC.Models;
using System.Diagnostics;
using Ninject;
using System.Security.Claims;

namespace Project_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IKernel ninjectKernel = new StandardKernel(new IoC_BLL());

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            {
                var logic = ninjectKernel.Get<ILogic>();

                var Cards = logic.ShowCard();
                return View(Cards);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Telegram()
        {
            // Retrieve the user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Pass the user ID to the view
            return View(model: userId);
        }
    }
}