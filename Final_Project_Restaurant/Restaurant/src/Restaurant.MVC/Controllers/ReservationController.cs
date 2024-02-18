using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.ReservationExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ISliderService _sliderService;
        public ReservationController(IReservationService reservationService, ISliderService sliderService)
        {
            _reservationService = reservationService;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            ReservationViewModel viewModel = new ReservationViewModel()
            {
                Sliders = await _sliderService.GetAllAsync(x => x.IsDeleted == false && x.IsReservation),
            };
            return View(viewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservation)
        {
            try
            {
                await _reservationService.Create(reservation);
            }
            catch (ReservationNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch(InvalidReservatinDateException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            catch(Exception ex) { }
            return RedirectToAction("Index", "Home");
        }
      
    }
}
