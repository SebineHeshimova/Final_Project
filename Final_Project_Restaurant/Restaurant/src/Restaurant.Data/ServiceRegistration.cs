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
        }
    }
}
