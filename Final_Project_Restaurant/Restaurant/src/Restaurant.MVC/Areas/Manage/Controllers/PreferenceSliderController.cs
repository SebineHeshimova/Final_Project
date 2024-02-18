using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.PreferenceSliderExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PreferenceSliderController : Controller
    {
        private readonly IPreferenceSliderService _preferenceSliderService;

        public PreferenceSliderController(IPreferenceSliderService preferenceSliderService)
        {
            _preferenceSliderService = preferenceSliderService;
        }

        public async Task<IActionResult> Index()
        {
            var preferenceSlider = await _preferenceSliderService.GetAllAsync();
            return View(preferenceSlider);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreferenceSlider preferenceSlider)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _preferenceSliderService.CreateAsync(preferenceSlider);
            }
            catch (PreferenceSliderImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (PreferenceSliderImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (PreferenceSliderNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var preferenceSlider = await _preferenceSliderService.GetByIdAsync(w => w.Id == id);
            if (preferenceSlider == null) return View("Error");
            return View(preferenceSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PreferenceSlider preferenceSlider)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _preferenceSliderService.UpdateAsync(preferenceSlider);
            }
            catch (PreferenceSliderNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (PreferenceSliderImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (PreferenceSliderImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var preferenceSlider = await _preferenceSliderService.GetByIdAsync(s => s.Id == id);
            if (preferenceSlider == null) return View("Error");
            return View(preferenceSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PreferenceSlider preferenceSlider)
        {
            try
            {
                await _preferenceSliderService.DeleteAsync(preferenceSlider.Id);
            }
            catch (PreferenceSliderNullException ex)
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
                await _preferenceSliderService.SoftDelete(id);
            }
            catch (PreferenceSliderNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
