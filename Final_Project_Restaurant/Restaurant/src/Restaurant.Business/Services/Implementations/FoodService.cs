using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Restaurant.Business.CustomException.RestaurantException.BannerExceptions;
using Restaurant.Business.CustomException.RestaurantException.FoodExceptions;
using Restaurant.Business.CustomException.RestaurantException.OfferExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Implementations
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _env;
        public FoodService(IFoodRepository foodRepository, IWebHostEnvironment env, ICategoryRepository categoryRepository)
        {
            _foodRepository = foodRepository;
            _env = env;
            _categoryRepository = categoryRepository;
        }

		public DbSet<Category> Table => _categoryRepository.Table;

		public async Task CreateAsync(Food food)
        {
            if (!_categoryRepository.Table.Any(c => c.Id == food.CategoryId))
            {
                throw new FoodCategoryException("CategoryId", "Category not found!");
            }
            if (food.ImageFile != null)
            {
                if (food.ImageFile.ContentType != "image/jpeg" && food.ImageFile.ContentType != "image/png")
                {
                    throw new FoodImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (food.ImageFile.Length > 2097152)
                {
                    throw new FoodImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                food.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/foods", food.ImageFile);

            }
            else
            {
                throw new FoodNullException("ImageFile", "Entity cannot be null!");
            }
            food.CreatedDate = DateTime.UtcNow.AddHours(4);
            food.UpdatedDate = DateTime.UtcNow.AddHours(4);
            food.IsDeleted = false;
            await _foodRepository.CreateAsync(food);
            await _foodRepository.CommitAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var food = await _foodRepository.SingleAsync(f => f.Id == id);
            if (food == null) throw new FoodNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/foods", food.ImageUrl);
            _foodRepository.Delete(food);
            await _foodRepository.CommitAsync();
        }

        public async Task<List<Food>> GetAllAsync(Expression<Func<Food, bool>>? expression = null, params string[]? includes)
        {
            return await _foodRepository.GetAllWhere(expression, "Category").ToListAsync();
        }

        public async Task<Food> GetByIdAsync(Expression<Func<Food, bool>>? expression = null, params string[]? includes)
        {
            return await _foodRepository.SingleAsync(expression, "Category");
        }

        public async Task SoftDelete(int id)
        {
            var food = await _foodRepository.SingleAsync(f => f.Id == id);
            if (food == null) throw new FoodNullException("Entity cannot be null!");
            food.IsDeleted = !food.IsDeleted;
            await _foodRepository.CommitAsync();
        }

        public async Task UpdateAsync(Food food)
        {
            var existFood = await _foodRepository.SingleAsync(f => f.Id == food.Id);
            if (existFood == null) throw new FoodNullException("Entity cannot be null!");
            if (!_categoryRepository.Table.Any(c => c.Id == food.CategoryId))
            {
                throw new FoodCategoryException("CategoryId", "Category not found!");

            }
            if (food.ImageFile != null)
            {
                if (food.ImageFile.ContentType != "image/jpeg" && food.ImageFile.ContentType != "image/png")
                {
                    throw new FoodImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (food.ImageFile.Length > 2097152)
                {
                    throw new FoodImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/foods", existFood.ImageUrl);
                existFood.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/foods", food.ImageFile);
            }
            existFood.UpdatedDate= DateTime.UtcNow.AddHours(4);
            existFood.Name = food.Name;
            existFood.Composition = food.Composition;
            existFood.Price= food.Price;
            existFood.CategoryId=food.CategoryId;
            existFood.IsNew=food.IsNew;
            await _foodRepository.CommitAsync();
        }
    }
}
