using AutoMapper;
using Dishantha.Application.AutoMapper;
using Dishantha.Application.Sightings.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dishantha.Application
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, Action<Type, Type, ServiceLifetime> configureInterceptor = null)
        {

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            services                
                .AddScoped<ISightingService, SightingService>()           
                .AddSingleton(mapperConfig.CreateMapper());

            return services;
        }
        
    }
}
