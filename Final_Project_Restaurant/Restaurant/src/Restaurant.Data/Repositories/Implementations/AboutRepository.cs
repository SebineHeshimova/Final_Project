using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Repositories.Implementations
{
	public class AboutRepository : GenericRepository<About>, IAboutRepository
	{
		public AboutRepository(RestaurantDbContext context) : base(context)
		{
		}
	}
}
