using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Entiity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.DAL
{
    public class RestaurantDbContext:IdentityDbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }
       
        public DbSet<Slider> Sliders {  get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Wrapper> Wrappers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }  
        public DbSet<Order> Orders {  get; set; }
        public DbSet<OrderItem> OrdersItems { get; set;}
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PreferenceSlider> PreferenceSlider { get; set; }   
        public DbSet<PreferenceText> PreferenceText { get; set; }   
        public DbSet<Gallery> Gallerys { get; set; }
        public DbSet<Video> Videos { get; set; }
    }

}
