using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.CustomException.RestaurantException.VideoExceptions;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;

namespace Restaurant.MVC.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class VideoController : Controller
	{
		private readonly IVideoService _videoService;

		public VideoController(IVideoService videoService)
		{
			_videoService = videoService;
		}

		public async Task<IActionResult> Index()
		{
			var video= await _videoService.GetAllAsync();
			return View(video);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Video video)
		{
			if (!ModelState.IsValid) return View();
			try
			{
				await _videoService.CreateAsync(video);
			}
			catch (VideoImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (VideoImageLengthException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (VideoNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Update(int id)
		{
			var Video = await _videoService.GetByIdAsync(g => g.Id == id);
			if (Video == null) return View("Error");
			return View(Video);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(Video video)
		{
			if (!ModelState.IsValid) return View();
			try
			{
				await _videoService.UpdateAsync(video);
			}
			catch (VideoNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (VideoImageContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (VideoImageLengthException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int id)
		{
			var video = await _videoService.GetByIdAsync(g => g.Id == id);
			if (video == null) return View("Error");
			return View(video);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Video video)
		{
			try
			{
				await _videoService.DeleteAsync(video.Id);
			}
			catch (VideoNullException ex)
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
				await _videoService.SoftDelete(id);
			}
			catch (VideoNullException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (Exception ex) { }
			return RedirectToAction("Index");
		}
	}
}
