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

        public async Task CreateAsync(Slider slider)
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

        public async Task DeleteAsync(int id)
        {
            var slider=await _repository.SingleAsync(x => x.Id == id);
            if (slider == null) throw new SliderNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/sliders", slider.ImageUrl);
            _repository.Delete(slider);
            await _repository.CommitAsync();    
        }

        public async Task<List<Slider>> GetAllAsync(Expression<Func<Slider, bool>>? expression = null, params string[]? includes)
        {
            return  await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(Expression<Func<Slider, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task SoftDelete(int id)
        {
            var slider = await _repository.SingleAsync(x => x.Id == id);
            if (slider == null) throw new SliderNullException("Entity cannot be null!");
            slider.IsDeleted = !slider.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(Slider slider)
        {  
            var existSlider=await _repository.SingleAsync(s=>s.Id==slider.Id);
			if (existSlider == null) throw new SliderNullException("Entity cannot be null!");
			if (slider.ImageFile != null)
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
            existSlider.RedirectText = slider.RedirectText;
            existSlider.RedirectUrl= slider.RedirectUrl; 
            await _repository.CommitAsync();
        }
    }
}
