using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.ViewModels;

namespace Restaurant.MVC.Controllers
{
	public class ConfirmMailController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(ConfirmMailViewModel viewModel)
		{
			return View();
		}
	}
}
