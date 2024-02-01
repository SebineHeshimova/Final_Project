using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface ISliderService
    {
        Task CreateAsync(Slider slider);
        Task UpdateAsync(Slider slider);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<Slider>> GetAllAsync(Expression<Func<Slider, bool>>? expression = null, params string[]? includes);
        Task<Slider> GetByIdAsync(Expression<Func<Slider, bool>>? expression = null, params string[]? includes);
    }
}
