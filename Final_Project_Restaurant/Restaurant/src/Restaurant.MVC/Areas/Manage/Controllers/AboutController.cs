using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.AboutException;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class AboutController : Controller
	{
		private readonly IAboutService _aboutService;

		public AboutController(IAboutService aboutService)
		{
			_aboutService = aboutService;
		}

		public async Task<IActionResult> Index()
		{
			var about = await _aboutService.GetAllAsync();
			return View(about);
		}
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about)
        {
            if (!ModelState.IsValid) return View("Error");
            try
            {
                await _aboutService.CreateAsync(about);
            }
            catch (AboutNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (AboutImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (AboutImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
		public async Task<IActionResult> Update(int id)
		{
			var about = await _aboutService.GetByIdAsync(s => s.Id == id);
			if (about == null) return View("Error");
			return View(about);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(About about)
		{
			try
			{
				await _aboutService.UpdateAsync(about);
			}
			catch (AboutNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (AboutImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (AboutImageLengthException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> Delete(int id)
        {
            var about = await _aboutService.GetByIdAsync(s => s.Id == id);
            if (about == null) return View("Error");
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(About about)
        {
            try
            {
                await _aboutService.DeleteAsync(about.Id);
            }
            catch (AboutNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _aboutService.SoftDelete(id);
            }
            catch (AboutNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
