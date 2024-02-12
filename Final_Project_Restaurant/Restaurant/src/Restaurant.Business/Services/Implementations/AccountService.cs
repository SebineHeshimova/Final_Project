using Microsoft.AspNetCore.Identity;
using Restaurant.Business.CustomException.RestaurantException.AccountExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;

namespace Restaurant.Business.Services.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


		public async Task Login(AdminLoginViewModel viewModel)
		{ 
			AppUser user = null;
			user = await _userManager.FindByNameAsync(viewModel.UserName);
			if (user == null)
			{
				throw new InvalidUsernameAndPasswordException("","Invalid Username or Password!");
			}
			var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);
			if (!result.Succeeded)
			{
				throw new InvalidUsernameAndPasswordException("", "Invalid Username or Password!");
			}
			

		}
	}
}
