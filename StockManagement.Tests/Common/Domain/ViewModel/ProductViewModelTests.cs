using StockManagement.Common.Domain.Models.ViewModel;

namespace StockManagement.Tests.Common.Domain.ViewModel
{
    public class ProductViewModelTests
    {
        [Fact]
        public void ProductViewModel_InitializesProperties()
        {
            // Arrange & Act
            var product = new ProductViewModel();

            // Assert
            Assert.Null(product.Id);
            Assert.Null(product.StorageId);
            Assert.Null(product.Name);
            Assert.Null(product.Quantity);
            Assert.Null(product.Size);
            Assert.Null(product.Width);
            Assert.Null(product.CreationDate);
            Assert.Null(product.UpdateDate);
        }

        [Fact]
        public void ProductViewModel_SetProperties_StoresValues()
        {
            // Arrange
            var product = new ProductViewModel
            {
                Id = "123",
                StorageId = "storage-1",
                Name = "Test Product",
                Quantity = 10.5m,
                Size = 5.0m,
                Width = 2.0m,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now.AddHours(1)
            };

            // Act & Assert
            Assert.Equal("123", product.Id);
            Assert.Equal("storage-1", product.StorageId);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal(10.5m, product.Quantity);
            Assert.Equal(5.0m, product.Size);
            Assert.Equal(2.0m, product.Width);
            Assert.NotNull(product.CreationDate);
            Assert.NotNull(product.UpdateDate);
        }

        [Fact]
        public void ProductViewModel_CreationDate_InitializedCorrectly()
        {
            // Arrange
            var now = DateTime.Now;
            var product = new ProductViewModel
            {
                CreationDate = now
            };

            // Act & Assert
            Assert.Equal(now.Date, product.CreationDate.Value.Date);
        }

        [Fact]
        public void ProductViewModel_UpdateDate_InitializedCorrectly()
        {
            // Arrange
            var now = DateTime.Now;
            var product = new ProductViewModel
            {
                UpdateDate = now
            };

            // Act & Assert
            Assert.Equal(now.Date, product.UpdateDate.Value.Date);
        }
    }
}
