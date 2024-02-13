using Microsoft.AspNetCore.Identity;
using Restaurant.Business.CustomException.RestaurantException.AccountExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;

namespace Restaurant.Business.Services.Implementations
{
	public class AdminAccountService : IAdminAccountService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		public AdminAccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
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
				throw new InvalidUsernameOrPasswordException("","Invalid Username or Password!");
			}
			var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);
			if (!result.Succeeded)
			{
				throw new InvalidUsernameOrPasswordException("", "Invalid Username or Password!");
			}
			

		}
	}
}
