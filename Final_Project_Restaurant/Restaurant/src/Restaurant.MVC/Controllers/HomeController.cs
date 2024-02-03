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
                Sliders = await _sliderService.GetAllAsync(x=>x.IsDeleted==false),
                Banners=await _bannerService.GetAllAsync(x=>x.IsDeleted==false),
                Wrappers=await _wrapperService.GetAllAsync(x => x.IsDeleted == false),
                Abouts=await _aboutService.GetAllAsync(a=>a.IsDeleted==false),
            };
            return View(model);
        }

    }
}