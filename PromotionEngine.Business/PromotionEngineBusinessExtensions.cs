using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Business.Implementations;
using PromotionEngine.Business.Interface;
using System;
using System.Collections.Generic;
using static PromotionEngine.Models.Enums;

namespace PromotionEngine.Business
{
    public static class PromotionEngineBusinessExtensions
    {
        public static IServiceCollection AddPromotionEngineBusiness(this IServiceCollection services)
        {
            services.AddMediatR(typeof(PromotionEngineBusinessExtensions).Assembly);
            services.AddTransient<Promotion1>();
            //services.AddTransient<Promotion2>();
            //services.AddTransient<Promotion3>();
            //services.AddTransient<Promotion4>();
            services.AddTransient<Func<PromotionTypes, IPromotion>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case PromotionTypes.Promotion1:
                        return serviceProvider.GetService<Promotion1>();
                    //case PromotionTypes.Promotion2:
                    //    return serviceProvider.GetService<Promotion2>();
                    //case PromotionTypes.Promotion3:
                    //    return serviceProvider.GetService<Promotion3>();
                    //case PromotionTypes.Promotion4:
                    //    return serviceProvider.GetService<Promotion4>();
                    default:
                        throw new KeyNotFoundException();
                }
            });
            return services;
        }
    }
}
