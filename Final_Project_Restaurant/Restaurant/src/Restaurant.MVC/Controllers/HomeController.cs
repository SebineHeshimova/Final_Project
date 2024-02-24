using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IBannerService _bannerService;
        private readonly IWrapperService _wrapperService;
        private readonly IAboutService _aboutService;
        private readonly IOfferService _offerService;
        private readonly IFoodService _foodService;
        
        public HomeController(ISliderService sliderService, IBannerService bannerService, IWrapperService wrapperService, IAboutService aboutService, IOfferService offerService, IFoodService foodService)
        {
            _sliderService = sliderService;
            _bannerService = bannerService;
            _wrapperService = wrapperService;
            _aboutService = aboutService;
            _offerService = offerService;
            _foodService = foodService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel()
            {
                Sliders = await _sliderService.GetAllAsync(x => x.IsDeleted == false && x.IsHome),
                Banners = await _bannerService.GetAllAsync(x => x.IsDeleted == false),
                Wrappers = await _wrapperService.GetAllAsync(x => x.IsDeleted == false),
                Abouts = await _aboutService.GetAllAsync(a => a.IsDeleted == false),
                Offers = await _offerService.GetAllAsync(o => o.IsDeleted == false),
                Foods = await _foodService.GetAllAsync(f => f.IsNew == true && f.IsDeleted==false),
                
            };
            return View(model);
        }

    }
}