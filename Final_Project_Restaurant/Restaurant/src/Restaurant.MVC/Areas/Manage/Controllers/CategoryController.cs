using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.CategoryExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _categoryService.GetAllAsync();
            return View(category);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _categoryService.CreateAsync(category);
            }
            catch(CategoryNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(CategoryNameException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var category=await _categoryService.GetByIdAsync(c=>c.Id==id);
            if (category == null) return View("Error");
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _categoryService.UpdateAsync(category);
            }
            catch(CategoryNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(CategoryNameException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(c => c.Id == id);
            if (category == null) return View("Error");
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            try
            {
                await _categoryService.DeleteAsync(category.Id);
            }
            catch(CategoryNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            var category = await _categoryService.GetByIdAsync(c => c.Id == id);
            if (category == null) return View("Error");
            try
            {
                await _categoryService.SoftDelete(category.Id);
            }
            catch(CategoryNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

    }
}
