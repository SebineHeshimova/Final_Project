using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.PreferenceTextExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Manage")]
    public class PreferenceTextController : Controller
    {
        private readonly IPreferenceTextService _preferenceTextService;
       
        public PreferenceTextController(IPreferenceTextService preferenceTextService)
        {
            _preferenceTextService = preferenceTextService;
        }

        public async Task<IActionResult> Index()
        {
            var preferenceText = await _preferenceTextService.GetAllAsync();
            return View(preferenceText);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreferenceText preferenceText)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _preferenceTextService.CreateAsync(preferenceText);
            }
            catch (PreferenceTextNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var preferenceText = await _preferenceTextService.GetByIdAsync(p =>p.Id == id);
            if (preferenceText == null) return View("Error");
            return View(preferenceText);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PreferenceText preferenceText)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _preferenceTextService.UpdateAsync(preferenceText);
            }
            catch (PreferenceTextNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var preferenceText = await _preferenceTextService.GetByIdAsync(p=>p.Id == id);
            if (preferenceText == null) return View("Error");
            return View(preferenceText);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PreferenceText preferenceText)
        {
            try
            {
                await _preferenceTextService.DeleteAsync(preferenceText.Id);
            }
            catch (PreferenceTextNullException ex)
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
                await _preferenceTextService.SoftDelete(id);
            }
            catch (PreferenceTextNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
