using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[]? includes);
        Task<Category> GetByIdAsync(Expression<Func<Category, bool>>? expression = null, params string[]? includes);
    }
}
