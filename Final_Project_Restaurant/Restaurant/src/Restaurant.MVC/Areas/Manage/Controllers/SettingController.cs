using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.SettingException;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Manage")]
	public class SettingController : Controller
	{
		private readonly ISettingService _settingService;

		public SettingController(ISettingService settingService)
		{
			_settingService = settingService;
		}

		public async Task<IActionResult> Index()
		{
			var setting = await _settingService.GetAllAsync();
			return View(setting);
		}
		public async Task<IActionResult> Update(int id)
		{
			var setting = await _settingService.GetByIdAsync(s => s.Id == id);
			if (setting == null) return View("Error");
			return View(setting);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(Setting setting)
		{
            if (!ModelState.IsValid) return View();
            try
			{
				await _settingService.UpdateAsync(setting);
			}
			catch (SettingNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}

	}
}
