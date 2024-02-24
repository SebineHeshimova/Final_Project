using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.FoodExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Data.DAL;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Manage")]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly RestaurantDbContext _foodRepository;
        public FoodController(IFoodService foodService, RestaurantDbContext foodRepository)
        {
            _foodService = foodService;
            _foodRepository = foodRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            var datas = await _foodService.GetAllPaginatedAsync(page, 10);
            return View(datas);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _foodService.Table;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food)
        {
            ViewBag.Category = _foodService.Table;
            if (!ModelState.IsValid) return View();
            try
            {
                await _foodService.CreateAsync(food);
            }
            catch(FoodCategoryException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FoodImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FoodImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FoodNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Category = _foodService.Table;
            var food = await _foodService.GetByIdAsync(s => s.Id == id);
            if (food == null) return View("Error");
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Food food)
        {
            ViewBag.Category = _foodService.Table;
            if(!ModelState.IsValid) return View();
            try
            {
                await _foodService.UpdateAsync(food);
            }
            catch (FoodNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (FoodCategoryException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FoodImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FoodImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _foodService.GetByIdAsync(s => s.Id == id);
            if (food == null) return View("Error");
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Food food)
        {
            try
            {
                await _foodService.DeleteAsync(food.Id);
            }
            catch (FoodNullException ex)
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
                await _foodService.SoftDelete(id);
            }
            catch (FoodNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
