using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly ISliderService _sliderService;
		private readonly IAboutService _aboutService;
		private readonly IChefService _chefService;
		private readonly IFeedbackService _feedbackService;
        private readonly IPreferenceSliderService _preferenceSliderService;
        private readonly IPreferenceTextService _preferenceTextService;
        public AboutController(ISliderService sliderService, IAboutService aboutService, IChefService chefService, IFeedbackService feedbackService, IPreferenceSliderService preferenceSliderService, IPreferenceTextService preferenceTextService)
        {
            _sliderService = sliderService;
            _aboutService = aboutService;
            _chefService = chefService;
            _feedbackService = feedbackService;
            _preferenceSliderService = preferenceSliderService;
            _preferenceTextService = preferenceTextService;
        }

        public async Task<IActionResult> Index()
        {
            AboutViewModel viewModel = new AboutViewModel()
            {
                Sliders = await _sliderService.GetAllAsync(x => x.IsDeleted == false && x.IsAbout),
                Abouts = await _aboutService.GetAllAsync(a => a.IsDeleted == false),
                Chefs = await _chefService.GetAllAsync(c => c.IsDeleted == false),
                Feedbacks = await _feedbackService.GetAllAsync(f => f.IsDeleted == false),
                PreferenceSliders = await _preferenceSliderService.GetAllAsync(p=>p.IsDeleted==false),
                PreferenceTexts = await _preferenceTextService.GetAllAsync(p => p.IsDeleted == false),
			};

            return View(viewModel);
        }
    }
}
