using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.CategoryExceptions;
using Restaurant.Business.CustomException.RestaurantException.OfferExceptions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Category category)
        {
            if (category == null) throw new CategoryNullException( "Entity cannot be null!");
            if (_repository.Table.Any(c=>c.Name==category.Name)) throw new CategoryNameException("Name","This category exists!");
            await _repository.CreateAsync(category);
            await _repository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category=await _repository.SingleAsync(c=>c.Id==id);
            if (category == null) throw new CategoryNullException("Entity cannot be null!");
            _repository.Delete(category);
            await _repository.CommitAsync();
        }

        public async Task<List<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Expression<Func<Category, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task SoftDelete(int id)
        {
            var category = await _repository.SingleAsync(o => o.Id == id);
            if (category == null) throw new CategoryNullException("Entity cannot be null!");
            category.IsDeleted = !category.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var existCategory=await _repository.SingleAsync(c=>c.Id==category.Id);
            if(existCategory == null) throw new CategoryNullException("Entity cannot be null!");
            if (_repository.Table.Any(c=>c.Id!=category.Id && c.Name==category.Name)) throw new CategoryNameException("Name","This category exists!");
            existCategory.Name=category.Name;
            await _repository.CommitAsync();
        }
    }
}
