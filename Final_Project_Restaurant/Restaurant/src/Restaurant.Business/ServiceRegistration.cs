﻿using Microsoft.Extensions.DependencyInjection;
using Restaurant.Business.Services.Implementations;
using Restaurant.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISliderService, SliderService>();
        }
    }
}