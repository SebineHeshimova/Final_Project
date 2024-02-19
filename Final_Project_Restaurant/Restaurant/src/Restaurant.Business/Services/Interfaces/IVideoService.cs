using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
	public interface IVideoService
	{
		Task CreateAsync(Video video);
		Task UpdateAsync(Video video);
		Task DeleteAsync(int id);
		Task SoftDelete(int id);
		Task<List<Video>> GetAllAsync(Expression<Func<Video, bool>>? expression = null, params string[]? includes);
		Task<Video> GetByIdAsync(Expression<Func<Video, bool>>? expression = null, params string[]? includes);
	}
}
