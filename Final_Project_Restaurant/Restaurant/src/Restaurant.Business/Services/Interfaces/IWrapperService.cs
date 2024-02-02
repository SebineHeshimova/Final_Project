using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
	public interface IWrapperService
	{
		Task CreateAsync(Wrapper wrapper);
		Task UpdateAsync(Wrapper wrapper);
		Task DeleteAsync(int id);
		Task SoftDelete(int id);
		Task<List<Wrapper>> GetAllAsync(Expression<Func<Wrapper, bool>>? expression = null, params string[]? includes);
		Task<Wrapper> GetByIdAsync(Expression<Func<Wrapper, bool>>? expression = null, params string[]? includes);
	}
}
