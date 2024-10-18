using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace StockManagement.Tests.Common.Redis
{
    public class RedisTests
    {
        [Fact]
        public void RegisterRedis_ShouldRegisterConnectionMultiplexer()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var result = StockManagement.Common.Redis.Redis.RegisterRedis(services);

            // Assert
            var serviceProvider = result.BuildServiceProvider();
            var connectionMultiplexer = serviceProvider.GetService<IConnectionMultiplexer>();

            Assert.NotNull(connectionMultiplexer);
        }

        [Fact]
        public void RegisterRedisHealthCheck_ShouldAddHealthCheck()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddLogging();

            // Act
            var result = StockManagement.Common.Redis.Redis.RegisterRedisHealthCheck(services);

            // Assert
            var serviceProvider = result.BuildServiceProvider();
            var healthChecks = serviceProvider.GetService<HealthCheckService>();

            Assert.NotNull(healthChecks);
        }
    }
}
