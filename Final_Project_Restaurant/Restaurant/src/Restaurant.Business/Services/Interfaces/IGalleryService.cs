using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
	public interface IGalleryService
	{
		Task CreateAsync(Gallery gallery);
		Task UpdateAsync(Gallery gallery);
		Task DeleteAsync(int id);
		Task SoftDelete(int id);
		Task<List<Gallery>> GetAllAsync(Expression<Func<Gallery, bool>>? expression = null, params string[]? includes);
		Task<Gallery> GetByIdAsync(Expression<Func<Gallery, bool>>? expression = null, params string[]? includes);
	}
}
