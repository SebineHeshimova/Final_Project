using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.ChefExceptions;
using Restaurant.Business.CustomException.RestaurantException.OfferExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Manage")]
    public class ChefController : Controller
    {
        private readonly IChefService _chefService;

        public ChefController(IChefService chefService)
        {
            _chefService = chefService;
        }

        public async Task<IActionResult> Index()
        {
            var chef = await _chefService.GetAllAsync();
            return View(chef);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chef chef)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _chefService.CreateAsync(chef);
            }
            catch (ChefNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (ChefImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (ChefImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var chef = await _chefService.GetByIdAsync(c => c.Id == id);
            if (chef == null) return View("Error");
            return View(chef);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Chef chef)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _chefService.UpdateAsync(chef);
            }
            catch (ChefNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (ChefImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (ChefImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var chef = await _chefService.GetByIdAsync(c => c.Id == id);
            if (chef == null) return View("Error");
            return View(chef);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Chef chef)
        {
            try
            {
                await _chefService.DeleteAsync(chef.Id);
            }
            catch (ChefNullException ex)
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
                await _chefService.SoftDelete(id);
            }
            catch (ChefNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
