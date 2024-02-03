using Restaurant.Core.Entiity;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Interfaces
{
	public interface IAboutService
	{
		Task CreateAsync(About about);
		Task UpdateAsync(About about);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<About>> GetAllAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes);
		Task<About> GetByIdAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes);
	}
}
