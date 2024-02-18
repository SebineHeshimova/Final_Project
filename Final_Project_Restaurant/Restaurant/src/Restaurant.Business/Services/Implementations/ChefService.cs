using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.BannerExceptions;
using Restaurant.Business.CustomException.RestaurantException.ChefExceptions;
using Restaurant.Business.CustomException.RestaurantException.OfferExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class ChefService : IChefService
    {
        private readonly IChefRepository _repository;
        private readonly IWebHostEnvironment _env;
        public ChefService(IChefRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task CreateAsync(Chef chef)
        {
            if (chef.ImageFile != null)
            {
                if (chef.ImageFile.ContentType != "image/jpeg" && chef.ImageFile.ContentType != "image/png")
                {
                    throw new ChefImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (chef.ImageFile.Length > 2097152)
                {
                    throw new ChefImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                chef.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/chefs", chef.ImageFile);
            }
            else
            {
                throw new ChefNullException("ImageFile", "Entity cannot be null!");
            }
            chef.CreatedDate = DateTime.UtcNow.AddHours(4);
            chef.UpdatedDate = DateTime.UtcNow.AddHours(4);
            chef.IsDeleted = false;
            await _repository.CreateAsync(chef);
            await _repository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chef = await _repository.SingleAsync(c => c.Id == id);
            if (chef == null) throw new ChefNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/chefs", chef.ImageUrl);
            _repository.Delete(chef);
            await _repository.CommitAsync();
        }

        public async Task<List<Chef>> GetAllAsync(Expression<Func<Chef, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<Chef> GetByIdAsync(Expression<Func<Chef, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task SoftDelete(int id)
        {
            var chef = await _repository.SingleAsync(c => c.Id == id);
            if (chef == null) throw new ChefNullException("Entity cannot be null!");
            chef.IsDeleted = !chef.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(Chef chef)
        {
            var existChef = await _repository.SingleAsync(c => c.Id == chef.Id);
            if (chef == null) throw new ChefNullException("Entity cannot be null!");
            if (chef.ImageFile != null)
            {
                if (chef.ImageFile.ContentType != "image/jpeg" && chef.ImageFile.ContentType != "image/png")
                {
                    throw new ChefImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (chef.ImageFile.Length > 2097152)
                {
                    throw new ChefImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/chefs", existChef.ImageUrl);
                existChef.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/chefs", chef.ImageFile);
            }
            existChef.UpdatedDate = DateTime.UtcNow.AddHours(4);
            existChef.FullName = chef.FullName;
            existChef.Position = chef.Position;
            await _repository.CommitAsync();
        }
    }
}
