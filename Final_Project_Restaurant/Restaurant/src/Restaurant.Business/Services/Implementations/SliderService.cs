using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException;
using Restaurant.Business.CustomException.RestaurantException.SliderException;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISliderRepository _repository;
        public SliderService(IWebHostEnvironment env, ISliderRepository repository)
        {
            _env = env;
            _repository = repository;
        }

        public async Task CreateSlider(Slider slider)
        {
            if (slider == null) throw new SliderNullException("Entity cannot be null!");
            if(slider.ImageFile != null)
            {
                if(slider.ImageFile.ContentType!="image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    throw new SliderImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    throw new SliderImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                slider.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/sliders", slider.ImageFile) ;
            }
            slider.CreatedDate = DateTime.UtcNow.AddHours(4);
            slider.UpdatedDate= DateTime.UtcNow.AddHours(4);
            slider.IsDeleted = false;
            await _repository.CreateAsync(slider);
            await _repository.CommitAsync();
        }

        public async Task DeleteSlider(int id)
        {
            var existSlider=await _repository.SingleAsync(x => x.Id == id);
            if (existSlider == null) throw new SliderNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/sliders", existSlider.ImageUrl);
            _repository.Delete(existSlider);
            await _repository.CommitAsync();    
        }

        public async Task<List<Slider>> GetAllSliders(Expression<Func<Slider, bool>>? expression = null, params string[]? includes)
        {
            return  await _repository.GetAllWhere(s=>s.IsDeleted==false, includes).ToListAsync();
        }

        public async Task<Slider> GetByIdSliders(Expression<Func<Slider, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(s=>s.IsDeleted==false, includes);
        }

        public async Task SoftDelete(Slider slider)
        {
            var existSlider = await _repository.SingleAsync(x => x.Id == slider.Id);
            if (existSlider == null) throw new SliderNullException("Entity cannot be null!");
            existSlider.IsDeleted = true;
            await _repository.CommitAsync();
        }

        public async Task UpdateSlider(Slider slider)
        {
            var existSlider=await _repository.SingleAsync(s=>s.Id==slider.Id);
            if(slider != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    throw new SliderImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    throw new SliderImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/sliders", existSlider.ImageUrl);
                existSlider.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/sliders", slider.ImageFile);
            }
            existSlider.UpdatedDate = DateTime.UtcNow.AddHours(4);
            existSlider.Title = slider.Title;
            existSlider.Description=slider.Description;
            existSlider.RedirectUrl = slider.RedirectUrl;
            await _repository.CommitAsync();
        }
    }
}
