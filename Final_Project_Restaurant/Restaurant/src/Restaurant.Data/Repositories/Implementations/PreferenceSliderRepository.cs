using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories.Implementations
{
    public class PreferenceSliderRepository : GenericRepository<PreferenceSlider>, IPreferenceSliderRepository
    {
        public PreferenceSliderRepository(RestaurantDbContext context) : base(context)
        {
        }
    }
}
