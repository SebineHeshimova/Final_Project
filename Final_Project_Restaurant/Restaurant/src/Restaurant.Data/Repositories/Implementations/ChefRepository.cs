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
    public class ChefRepository : GenericRepository<Chef>, IChefRepository
    {
        public ChefRepository(RestaurantDbContext context) : base(context)
        {
        }
    }
}
