#region

using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;

#endregion

namespace StockManagement.Common.Redis;

public static class Redis
{
    public static IServiceCollection RegisterRedis(this IServiceCollection services)
    {
        var options = new ConfigurationOptions
        {
            AbortOnConnectFail = true,
            AsyncTimeout = 60000,// config
            ClientName = "StockManagement",// config
            ConnectRetry = 3,
            ConnectTimeout = 60000,// config
            DefaultDatabase = 0, // config
            EndPoints = new EndPointCollection
                {$"localhost:6379"}, // config
            Password = "",
            ReconnectRetryPolicy = new ExponentialRetry(5000, 15000),
            SyncTimeout = 60000,
            User = ""
        };

        try
        {
            var connection = ConnectionMultiplexer.Connect(options);

            services.AddSingleton<IConnectionMultiplexer>(connection);
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Redis connection error");

            throw;
        }

        return services;
    }

    public static IServiceCollection RegisterRedisHealthCheck(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddRedis($"{"localhost"}:{6379},password={""},user={""},syncTimeout={60000},connectTimeout={60000},abortConnect=false", "Redis");

        return services;
    }
}