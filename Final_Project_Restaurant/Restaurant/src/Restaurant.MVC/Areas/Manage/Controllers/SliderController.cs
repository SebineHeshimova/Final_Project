using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.SliderException;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Manage")]
	public class SliderController : Controller
	{
		private readonly ISliderService _sliderService;

		public SliderController(ISliderService sliderService)
		{
			_sliderService = sliderService;
		}

		public async Task<IActionResult> Index()
		{
			var sliders=await _sliderService.GetAllAsync();
			return View(sliders);
		}
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Slider slider)
		{
			if (!ModelState.IsValid) return View();
			try
			{
				await _sliderService.CreateAsync(slider);
			}
			catch (SliderImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (SliderImageLengthException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
            }
            catch (SliderNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Update(int id)
		{
			var slider = await _sliderService.GetByIdAsync(s => s.Id == id);
			if (slider == null) return View("Error");
			return View(slider);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(Slider slider)
		{
            if (!ModelState.IsValid) return View();
            try
			{
				await _sliderService.UpdateAsync(slider);
			}
			catch (SliderNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (SliderImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (SliderImageLengthException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			var slider = await _sliderService.GetByIdAsync(s => s.Id == id);
			if (slider == null) return View("Error");
			return View(slider);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Slider slider)
		{
			try
			{
				await _sliderService.DeleteAsync(slider.Id);
			}
			catch (SliderNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View() ;
			}
			catch(Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> SoftDelete(int id)
		{
			try
			{
				await _sliderService.SoftDelete(id);
			}
			catch (SliderNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch(Exception ex) { }
			return RedirectToAction("Index");
		}
	}
}
