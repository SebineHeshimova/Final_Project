using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.MVC.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;

		public HomeController(ISliderService sliderService)
		{
			_sliderService = sliderService;
		}

		public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel()
            {
                Sliders = await _sliderService.GetAllAsync()
            };
            return View(model);
        }

    }
}