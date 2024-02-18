using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Data.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public static class ServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IWrapperRepository, WrapperRepository>();
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IChefRepository, ChefRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        }
    }
}
