using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.AccountExceptions.UserAccountExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserAccountService _accountService;

        public AccountController(IUserAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
		{
			return View();
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
		{
			if (!ModelState.IsValid) return View();
			try
			{
				await _accountService.Register(viewModel);
			}
			catch(InvalidUserUsernameException ex)
			{
			    ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch(InvalidUserEmailException ex)
			{
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
			catch(InvalidUserPasswordException ex)
			{
                ModelState.AddModelError("", ex.Message);
                return View();
            }
			catch (Exception ex) { }
            return RedirectToAction("index", "Home");
        }
	}
}
