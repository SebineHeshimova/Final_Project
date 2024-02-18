using Microsoft.AspNetCore.Hosting;
using Restaurant.Business.CustomException.RestaurantException.PreferenceSliderExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Business.Services.Implementations
{

    public class PreferenceSliderService : IPreferenceSliderService
    {
        private readonly IPreferenceSliderRepository _repository;
        private readonly IWebHostEnvironment _env;
        public PreferenceSliderService(IPreferenceSliderRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task CreateAsync(PreferenceSlider preferenceSlider)
        {
            if (preferenceSlider.ImageFile != null)
            {
                if (preferenceSlider.ImageFile.ContentType != "image/jpeg" && preferenceSlider.ImageFile.ContentType != "image/png")
                {
                    throw new PreferenceSliderImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (preferenceSlider.ImageFile.Length > 2097152)
                {
                    throw new PreferenceSliderImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                preferenceSlider.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/preferenceSliders", preferenceSlider.ImageFile);
            }
            else
            {
                throw new PreferenceSliderNullException("ImageFile", "Entity cannot be null!");
            }
            preferenceSlider.CreatedDate = DateTime.UtcNow.AddHours(4);
            preferenceSlider.UpdatedDate = DateTime.UtcNow.AddHours(4);
            preferenceSlider.IsDeleted = false;
            await _repository.CreateAsync(preferenceSlider);
            await _repository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var preferenceSlider = await _repository.SingleAsync(x => x.Id == id);
            if (preferenceSlider == null) throw new PreferenceSliderNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/preferenceSliders", preferenceSlider.ImageUrl);
            _repository.Delete(preferenceSlider);
            await _repository.CommitAsync();
        }

        public Task<List<PreferenceSlider>> GetAllAsync(Expression<Func<PreferenceSlider, bool>>? expression = null, params string[]? includes)
        {
            return _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<PreferenceSlider> GetByIdAsync(Expression<Func<PreferenceSlider, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task SoftDelete(int id)
        {
            var preferenceSlider = await _repository.SingleAsync(x => x.Id == id);
            if (preferenceSlider == null) throw new PreferenceSliderNullException("Entity cannot be null!");
            preferenceSlider.IsDeleted = !preferenceSlider.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(PreferenceSlider preferenceSlider)
        {
            var existPreferenceSlider = await _repository.SingleAsync(p => p.Id == preferenceSlider.Id);
            if (preferenceSlider == null) throw new PreferenceSliderNullException("Entity cannot be null!");
            if (preferenceSlider.ImageFile != null)
            {
                if (preferenceSlider.ImageFile.ContentType != "image/jpeg" && preferenceSlider.ImageFile.ContentType != "image/png")
                {
                    throw new PreferenceSliderImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (preferenceSlider.ImageFile.Length > 2097152)
                {
                    throw new PreferenceSliderImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/preferenceSliders", existPreferenceSlider.ImageUrl);
                existPreferenceSlider.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/preferenceSliders", preferenceSlider.ImageFile);
            }
            existPreferenceSlider.UpdatedDate = DateTime.UtcNow.AddHours(4);
            existPreferenceSlider.Title = preferenceSlider.Title;
            existPreferenceSlider.Description = preferenceSlider.Description;
            await _repository.CommitAsync();
        }
    }
}
