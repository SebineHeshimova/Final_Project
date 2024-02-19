using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Business.ViewModels;

namespace Restaurant.MVC.Controllers
{
    public class GalleryController : Controller
    {
        private readonly  ISliderService _sliderService;
        private readonly IGalleryService _galleryService;
        private readonly IVideoService _videoService;
        public GalleryController(ISliderService sliderService, IGalleryService galleryService, IVideoService videoService)
        {
            _sliderService = sliderService;
            _galleryService = galleryService;
            _videoService = videoService;
        }

        public async Task<IActionResult> Index()
        {
            GalleryViewModel viewModel = new GalleryViewModel()
            {
                Sliders = await _sliderService.GetAllAsync(s => s.IsDeleted == false && s.IsGallery==true),
                Galleries= await _galleryService.GetAllAsync(g=>g.IsDeleted==false),
                Videos=await _videoService.GetAllAsync(v=>v.IsDeleted==false)
            };
            return View(viewModel);
        }
    }
}
