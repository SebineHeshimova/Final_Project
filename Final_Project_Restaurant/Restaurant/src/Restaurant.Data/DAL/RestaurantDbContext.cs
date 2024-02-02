using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.DAL
{
    public class RestaurantDbContext:DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }
       
        public DbSet<Slider> Sliders {  get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Wrapper> Wrappers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<About> Abouts { get; set; }
    }
}
