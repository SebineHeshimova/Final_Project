using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.SliderException;
using Restaurant.Business.CustomException.RestaurantException.WrapperExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class WrapperController : Controller
	{
		private readonly IWrapperService _wrapperService;

		public WrapperController(IWrapperService wrapperService)
		{
			_wrapperService = wrapperService;
		}

		public async Task<IActionResult> Index()
		{
			var wrapper=await _wrapperService.GetAllAsync();
			return View(wrapper);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Wrapper wrapper)
		{
			if (!ModelState.IsValid) return View();
			try
			{
				await _wrapperService.CreateAsync(wrapper);
			}
			catch (WrapperImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (WrapperImageLengthException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
            }
            catch (WrapperNullException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Update(int id)
		{
			var wrapper = await _wrapperService.GetByIdAsync(w => w.Id == id);
			if (wrapper == null) return View("Error");
			return View(wrapper);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(Wrapper wrapper)
		{
            if (!ModelState.IsValid) return View();
            try
			{
				await _wrapperService.UpdateAsync(wrapper);
			}
			catch (WrapperNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (WrapperImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (WrapperImageLengthException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			var wrapper = await _wrapperService.GetByIdAsync(s => s.Id == id);
			if (wrapper == null) return View("Error");
			return View(wrapper);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Wrapper wrapper)
		{
			try 
			{
				await _wrapperService.DeleteAsync(wrapper.Id);
			}
			catch (WrapperNullException ex)
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
				await _wrapperService.SoftDelete(id);
			}
			catch (WrapperNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
	}
}
