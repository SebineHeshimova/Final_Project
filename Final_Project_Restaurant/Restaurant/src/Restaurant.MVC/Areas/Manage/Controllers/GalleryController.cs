using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.GalleryExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Manage")]
	public class GalleryController : Controller
	{
		private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        public async Task<IActionResult> Index()
		{
			var gallery=await _galleryService.GetAllAsync();
			return View(gallery);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Gallery gallery)
		{
			if (!ModelState.IsValid) return View();
			try
			{
				await _galleryService.CreateAsync(gallery);
			}
			catch(GalleryImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch(GalleryImageLengthException ex)
			{
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
			catch(GalleryNullException ex)
			{
                ModelState.AddModelError("", ex.Message);
                return View();
            }
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Update(int id)
		{
            var gallery = await _galleryService.GetByIdAsync(g => g.Id == id);
            if (gallery == null) return View("Error");
            return View(gallery);
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(Gallery gallery)
		{
            if (!ModelState.IsValid) return View();
            try
            {
                await _galleryService.UpdateAsync(gallery);
            }
            catch (GalleryNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (GalleryImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (GalleryImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
		public async Task<IActionResult> Delete(int id)
        {
            var gallery = await _galleryService.GetByIdAsync(g =>g.Id == id);
            if (gallery == null) return View("Error");
            return View(gallery);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Gallery gallery)
        {
            try
            {
                await _galleryService.DeleteAsync(gallery.Id);
            }
            catch (GalleryNullException ex)
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
                await _galleryService.SoftDelete(id);
            }
            catch (GalleryNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
	}
}
