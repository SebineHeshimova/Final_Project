using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.BannerExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Implementations
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _repository;
        private readonly IWebHostEnvironment _env;
        public BannerService(IBannerRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

       
        public async Task CreateAsync(Banner banner)
        {
            if (banner.ImageFile != null)
            {
                if (banner.ImageFile.ContentType != "image/jpeg" && banner.ImageFile.ContentType != "image/png")
                {
                    throw new BannerImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (banner.ImageFile.Length > 2097152)
                {
                    throw new BannerImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                banner.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/banners", banner.ImageFile);
            }
            else
            {
                throw new BannerNullException("ImageFile", "Entity cannot be null!");
            }
            banner.CreatedDate = DateTime.UtcNow.AddHours(4);
            banner.UpdatedDate = DateTime.UtcNow.AddHours(4);
            banner.IsDeleted = false;
            await _repository.CreateAsync(banner);
            await _repository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var banner = await _repository.SingleAsync(x => x.Id == id);
            if (banner == null) throw new BannerNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/banners", banner.ImageUrl);
            _repository.Delete(banner);
            await _repository.CommitAsync();
        }

        public async Task<List<Banner>> GetAllAsync(Expression<Func<Banner, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<Banner> GetByIdAsync(Expression<Func<Banner, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task SoftDelete(int id)
        {
            var banner = await _repository.SingleAsync(x => x.Id == id);
            if (banner == null) throw new BannerNullException("Entity cannot be null!");
            banner.IsDeleted = !banner.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(Banner banner)
        {
            var existBanner = await _repository.SingleAsync(s => s.Id == banner.Id);
            if (existBanner == null) throw new BannerNullException("Entity cannot be null!");
            if (banner.ImageFile != null)
            {
                if (banner.ImageFile.ContentType != "image/jpeg" && banner.ImageFile.ContentType != "image/png")
                {
                    throw new BannerImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (banner.ImageFile.Length > 2097152)
                {
                    throw new BannerImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/banners", existBanner.ImageUrl);
                existBanner.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/banners", banner.ImageFile);
            }
            existBanner.UpdatedDate = DateTime.UtcNow.AddHours(4);
            existBanner.Title = banner.Title;
            existBanner.Description = banner.Description;
            existBanner.RedirectUrl = banner.RedirectUrl;
            await _repository.CommitAsync();
        }
    }
}
