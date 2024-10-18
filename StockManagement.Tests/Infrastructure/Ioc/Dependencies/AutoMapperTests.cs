using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace StockManagement.Tests.Infrastructure.Ioc.Dependencies
{
    public class AutoMapperTests
    {
        [Fact]
        public void RegisterAutoMapper_ShouldRegisterMapper()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var result = StockManagement.Ioc.Dependencies.AutoMapper.RegisterAutoMapper(services);
            var serviceProvider = result.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();

            // Assert
            Assert.NotNull(mapper);
            Assert.IsType<Mapper>(mapper);
        }
    }
}
