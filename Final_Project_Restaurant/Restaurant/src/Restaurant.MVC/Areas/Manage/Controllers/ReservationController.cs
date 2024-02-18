using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.ReservationExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using System.Security.Permissions;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var reservation = await _reservationService.GetAllAsync();
            return View(reservation);
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
            catch(Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
