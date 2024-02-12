using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class DashboardController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> CreateAdmin()
		{
			AppUser appUser = new AppUser()
			{
				FullName = "Hashimova Sabina",
				UserName = "SabinaHS"
			};
			await _userManager.CreateAsync(appUser, "Sabina123@");
			await _userManager.AddToRoleAsync(appUser, "SuperAdmin");
			return Ok();
		}
		public async Task<IActionResult> CreateRole()
		{
			IdentityRole role1 = new IdentityRole("SuperAdmin");
			IdentityRole role2 = new IdentityRole("Admin");
			IdentityRole role3 = new IdentityRole("User");

			await _roleManager.CreateAsync(role1);
			await _roleManager.CreateAsync(role2);
			await _roleManager.CreateAsync(role3);
			return Ok("Role yarandi!");
		}
	}
}
