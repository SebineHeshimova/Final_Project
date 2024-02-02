using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
	public interface IAboutService
	{
		Task CreateAsync(About about);
		Task UpdateAsync(About about);
		Task<List<About>> GetAllAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes);
		Task<About> GetByIdAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes);
	}
}
