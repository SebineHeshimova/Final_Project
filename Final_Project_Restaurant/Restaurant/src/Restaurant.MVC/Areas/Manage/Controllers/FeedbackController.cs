using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.FeedbackExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<IActionResult> Index()
        {
            var feedback= await _feedbackService.GetAllAsync();
            return View(feedback);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _feedbackService.CreateAsync(feedback);
            }
            catch (FeedbackNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (FeedbackImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FeedbackImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var feedback = await _feedbackService.GetByIdAsync(f=>f.Id == id);
            if (feedback == null) return View("Error");
            return View(feedback);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Feedback feedback)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _feedbackService.UpdateAsync(feedback);
            }
            catch (FeedbackNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (FeedbackImageContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FeedbackImageLengthException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _feedbackService.GetByIdAsync(c => c.Id == id);
            if (feedback == null) return View("Error");
            return View(feedback);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Feedback feedback)
        {
            try
            {
                await _feedbackService.DeleteAsync(feedback.Id);
            }
            catch (FeedbackNullException ex)
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
                await _feedbackService.SoftDelete(id);
            }
            catch (FeedbackNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }
    }
}
