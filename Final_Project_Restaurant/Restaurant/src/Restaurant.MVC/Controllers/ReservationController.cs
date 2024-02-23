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
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservation)
        {
            if(!ModelState.IsValid) return View();
            try
            {
                await _reservationService.Create(reservation);
            }
            catch (ReservationNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (InvalidReservatinDateException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Update(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(r => r.Id == id);
            if (reservation == null) return View("Error");
            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Reservation reservation)
        {
            if (!ModelState.IsValid) return View(reservation);
            try
            {
                await _reservationService.Update(reservation);
            }
            catch (ReservationNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (InvalidReservatinDateException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(r => r.Id == id);
            if (reservation == null) return View("Error");
            return View(reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Reservation reservation)
        {
            try
            {
                await _reservationService.Delete(reservation.Id);
            }
            catch (ReservationNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
