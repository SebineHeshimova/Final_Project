using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.MVC.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
       private readonly IWrapperService _wrapperService;
        private readonly IAboutService _aboutService;
        public HomeController(ISliderService sliderService, IBannerService bannerService, IWrapperService wrapperService, IAboutService aboutService)
        {
            _sliderService = sliderService;
            _bannerService = bannerService;
            _wrapperService = wrapperService;
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Banners=await _bannerService.GetAllAsync(),
                Wrappers=await _wrapperService.GetByIdAsync(),
                Abouts=await _aboutService.GetByIdAsync(),
            };
            return View(model);
        }

    }
}