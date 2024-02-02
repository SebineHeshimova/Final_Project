using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.MVC.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
		public HomeController(ISliderService sliderService, IBannerService bannerService)
		{
			_sliderService = sliderService;
			_bannerService = bannerService;
		}

		public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Banners=await _bannerService.GetAllAsync()
            };
            return View(model);
        }

    }
}