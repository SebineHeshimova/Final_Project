using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.MVC.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class MenuController : Controller
    {

        private readonly IFoodService _foodService;
        private readonly ICategoryService _categoryService;
        private readonly ISliderService _sliderService;
        public MenuController(IFoodService foodService, ICategoryService categoryService, ISliderService sliderService)
        {
            _foodService = foodService;
            _categoryService = categoryService;
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {
            MenuViewModel model = new MenuViewModel()
            {
                Categories = await _categoryService.GetAllAsync(c => c.IsDeleted == false),
                Foods = await _foodService.GetAllAsync(f => f.IsDeleted == false),
                Sliders = await _sliderService.GetAllAsync(s=>s.IsDeleted==false && s.IsMenu)
            };
            return View(model);
        }
    }
}
