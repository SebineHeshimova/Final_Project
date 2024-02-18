using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;

namespace Restaurant.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IWrapperService, WrapperService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<IAdminAccountService, AdminAccountService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IChefService, ChefService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
        }
    }
}
