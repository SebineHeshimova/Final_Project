using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Interfaces
{
    public interface IPreferenceTextService
    {
        Task CreateAsync(PreferenceText preferenceText);
        Task UpdateAsync(PreferenceText preferenceText);
        Task DeleteAsync(int id);
        Task SoftDelete(int id);
        Task<List<PreferenceText>> GetAllAsync(Expression<Func<PreferenceText, bool>>? expression = null, params string[]? includes);
        Task<PreferenceText> GetByIdAsync(Expression<Func<PreferenceText, bool>>? expression = null, params string[]? includes);
    }
}
