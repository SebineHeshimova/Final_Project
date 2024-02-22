﻿using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.OrderExceptions;
using Restaurant.Business.CustomException.RestaurantException.ReservationExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ReservationCheckController : Controller
    {
        private readonly IReservationCheckService _reservationCheckService;

        public ReservationCheckController(IReservationCheckService reservationCheckService)
        {
            _reservationCheckService = reservationCheckService;
        }

        public async Task<IActionResult> Index()
        {
            List<Reservation> reservation = await _reservationCheckService.GetAllAsync();
            return View(reservation);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var reservation = await _reservationCheckService.Detail(id);
            return View(reservation);
        }
        public async Task<IActionResult> Accept(int id)
        {
            try
            {
                await _reservationCheckService.Accept(id);
            }
            catch (ReservationNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("index", "ReservationCheck");
        }
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                await _reservationCheckService.Reject(id);
            }
            catch (ReservationNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("index", "ReservationCheck");
        }
        public async Task<IActionResult> Pending(int id)
        {
            try
            {
                await _reservationCheckService.Pending(id);
            }
            catch( ReservationNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("index", "ReservationCheck");
        }
    }
}
