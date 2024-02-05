using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.BannerExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            var banner = await _bannerService.GetAllAsync();
            return View(banner);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _bannerService.CreateAsync(banner);
            }
            catch (BannerNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (BannerImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (BannerImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var banner = await _bannerService.GetByIdAsync(b => b.Id == id);
            if (banner == null) return View("Error");
            return View(banner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Banner banner)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _bannerService.UpdateAsync(banner);
            }
            catch (BannerNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (BannerImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (BannerImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _bannerService.GetByIdAsync(s => s.Id == id);
            if (banner == null) return View("Error");
            return View(banner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Banner banner)
        {
            try
            {
                await _bannerService.DeleteAsync(banner.Id);
            }
            catch (BannerNullException ex)
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
                await _bannerService.SoftDelete(id);
            }
            catch (BannerNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
