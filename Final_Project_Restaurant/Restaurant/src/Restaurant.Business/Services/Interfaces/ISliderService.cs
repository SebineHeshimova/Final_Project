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
        Task CreateSlider(Slider slider);
        Task UpdateSlider(Slider slider);
        Task DeleteSlider(int id);
        Task SoftDelete(Slider slider);
        Task<List<Slider>> GetAllSliders(Expression<Func<Slider, bool>>? expression = null, params string[]? includes);
        Task<Slider> GetByIdSliders(Expression<Func<Slider, bool>>? expression = null, params string[]? includes);
    }
}
