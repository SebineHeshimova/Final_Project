using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IBannerService
    {
        Task CreateAsync(Banner banner);
        Task UpdateAsync(Banner banner);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<Banner>> GetAllAsync(Expression<Func<Banner, bool>>? expression = null, params string[]? includes);
        Task<Banner> GetByIdAsync(Expression<Func<Banner, bool>>? expression = null, params string[]? includes);
    }
}
