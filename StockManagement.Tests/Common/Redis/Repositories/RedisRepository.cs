using Moq;
using StackExchange.Redis;
using StockManagement.Common.Redis.Repositories;

namespace StockManagement.Tests.Common.Redis.Repositories
{
    public class RedisRepositoryTests : IDisposable
    {
        private readonly Mock<IConnectionMultiplexer> _mockConnection;
        private readonly Mock<IDatabase> _mockDatabase;
        private readonly RedisRepository _repository;

        public RedisRepositoryTests()
        {
            _mockConnection = new Mock<IConnectionMultiplexer>();
            _mockDatabase = new Mock<IDatabase>();
            _mockConnection.Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_mockDatabase.Object);
            _repository = new RedisRepository(_mockConnection.Object);
        }

        [Fact]
        public async Task Adicionar_KeyDoesNotExist_ReturnsTrue()
        {
            // Arrange
            var chave = "testKey";
            var valor = "testValue";
            _mockDatabase.Setup(db => db.StringSetAsync(chave, valor, null, When.NotExists, CommandFlags.None))
                .ReturnsAsync(true);

            // Act
            var result = await _repository.Adicionar(chave, valor);

            // Assert
            Assert.True(result);
            _mockDatabase.Verify(db => db.StringSetAsync(chave, valor, null, When.NotExists, CommandFlags.None), Times.Once);
        }

        [Fact]
        public async Task Existe_KeyExists_ReturnsTrue()
        {
            // Arrange
            var chave = "testKey";
            _mockDatabase.Setup(db => db.KeyExistsAsync(chave, CommandFlags.None))
                .ReturnsAsync(true);

            // Act
            var result = await _repository.Existe(chave);

            // Assert
            Assert.True(result);
            _mockDatabase.Verify(db => db.KeyExistsAsync(chave, CommandFlags.None), Times.Once);
        }

        [Fact]
        public async Task Remover_KeyExists_ReturnsTrue()
        {
            // Arrange
            var chave = "testKey";
            _mockDatabase.Setup(db => db.KeyDeleteAsync(chave, CommandFlags.None)).ReturnsAsync(true);

            // Act
            var result = await _repository.Remover(chave);

            // Assert
            Assert.True(result);
            _mockDatabase.Verify(db => db.KeyDeleteAsync(chave, CommandFlags.None), Times.Once);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
