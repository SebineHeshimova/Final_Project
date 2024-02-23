using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.OfferExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
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
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Offer offer)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _offerService.CreateAsync(offer);
            }
            catch (OfferNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (OfferImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (OfferImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var offer = await _offerService.GetByIdAsync(s => s.Id == id);
            if (offer == null) return View("Error");
            return View(offer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Offer offer)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _offerService.UpdateAsync(offer);
            }
            catch (OfferNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (OfferImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (OfferImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _offerService.GetByIdAsync(s => s.Id == id);
            if (offer == null) return View("Error");
            return View(offer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Offer offer)
        {
            try
            {
                await _offerService.DeleteAsync(offer.Id);
            }
            catch (OfferNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _offerService.SoftDelete(id);
            }
            catch (OfferNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
