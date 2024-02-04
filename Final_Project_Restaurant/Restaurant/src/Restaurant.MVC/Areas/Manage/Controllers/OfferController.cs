using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public async Task<IActionResult> Index()
        {
            var offer = await _offerService.GetAllAsync();
            return View(offer);
        }

    }
}
