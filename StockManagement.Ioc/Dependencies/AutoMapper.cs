#region

using AutoMapper;
using StockManagement.Ioc.Mappers;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace StockManagement.Ioc.Dependencies;

public static class AutoMapper
{
    public static MapperConfiguration MapperConfigurationBuilder()
    {
        return new MapperConfiguration(mapperConfigurationExpression =>
            mapperConfigurationExpression
                .AddProfile(new ViewModelsProfiles())
        );
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddSingleton(MapperConfigurationBuilder().CreateMapper());

        return services;
    }
}