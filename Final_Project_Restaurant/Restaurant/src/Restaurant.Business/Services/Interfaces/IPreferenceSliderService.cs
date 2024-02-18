using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IPreferenceSliderService
    {
        Task CreateAsync(PreferenceSlider preferenceSlider);
        Task UpdateAsync(PreferenceSlider preferenceSlider);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<PreferenceSlider>> GetAllAsync(Expression<Func<PreferenceSlider, bool>>? expression = null, params string[]? includes);
        Task<PreferenceSlider> GetByIdAsync(Expression<Func<PreferenceSlider, bool>>? expression = null, params string[]? includes);
    }
}
