using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;

namespace Restaurant.MVC.Controllers
{
	public class ContactController : Controller
	{
		private readonly ISliderService _sliderService;

		public ContactController(ISliderService sliderService)
		{
			_sliderService = sliderService;
		}

		public async Task<IActionResult> Index()
		{
			ContactViewModel viewModel = new ContactViewModel()
			{
				Sliders = await _sliderService.GetAllAsync(s => s.IsDeleted == false && s.IsContact==true),
			};
			return View(viewModel);
		}
	}
}
