using Microsoft.EntityFrameworkCore;
using Restaurant.Business.PaginationModel;
using Restaurant.Core.Entiity;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IFoodService
    {

        public DbSet<Category> Table {  get; } 
		Task CreateAsync(Food food);
        Task UpdateAsync(Food food);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<Food>> GetAllAsync(Expression<Func<Food, bool>>? expression = null, params string[]? includes);
        Task<Food> GetByIdAsync(Expression<Func<Food, bool>>? expression = null, params string[]? includes);

        Task<PaginatedList<Food>> GetAllPaginatedAsync(int page, int pageSize, Expression<Func<Food, bool>>? expression = null, params string[]? includes);
    }
}
