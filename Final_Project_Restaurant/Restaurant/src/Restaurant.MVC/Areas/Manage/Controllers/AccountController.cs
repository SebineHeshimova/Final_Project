using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.AccountExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class AccountController : Controller
	{
		private readonly IAdminAccountService _accountService;

		public AccountController(IAdminAccountService accountService)
		{
			_accountService = accountService;
		}

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AdminLoginViewModel viewModel)
		{
            if (!ModelState.IsValid) return View();
            try
			{
				await _accountService.Login(viewModel);
			}
			catch (InvalidUsernameAndPasswordException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch(Exception ex) { }
			return RedirectToAction("Index", "Dashboard");
		}
	}
}
