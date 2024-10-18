using AutoMapper;
using StockManagement.Common.Domain.Models.ViewModel;
using StockManagement.Ioc.Mappers;

namespace StockManagement.Tests.Ioc.Mappers
{
    public class ViewModelsProfilesTests
    {
        private readonly IMapper _mapper;

        public ViewModelsProfilesTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ViewModelsProfiles>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void PostProductViewModel_To_ProductViewModel_MappingIsCorrect()
        {
            // Arrange
            var postProductViewModel = new PostProductViewModel
            {
                Id = "1",
                StorageId = "Storage1",
                Name = "Product1",
                Quantity = 10,
                Size = 5,
                Width = 2
            };

            // Act
            var productViewModel = _mapper.Map<ProductViewModel>(postProductViewModel);

            // Assert
            Assert.Equal(postProductViewModel.Id, productViewModel.Id);
            Assert.Equal(postProductViewModel.StorageId, productViewModel.StorageId);
            Assert.Equal(postProductViewModel.Name, productViewModel.Name);
            Assert.Equal(postProductViewModel.Quantity, productViewModel.Quantity);
            Assert.Equal(postProductViewModel.Size, productViewModel.Size);
            Assert.Equal(postProductViewModel.Width, productViewModel.Width);
        }

        [Fact]
        public void ProductViewModel_To_PostProductViewModel_MappingIsCorrect()
        {
            // Arrange
            var productViewModel = new ProductViewModel
            {
                Id = "1",
                StorageId = "Storage1",
                Name = "Product1",
                Quantity = 10,
                Size = 5,
                Width = 2
            };

            // Act
            var postProductViewModel = _mapper.Map<PostProductViewModel>(productViewModel);

            // Assert
            Assert.Equal(productViewModel.Id, postProductViewModel.Id);
            Assert.Equal(productViewModel.StorageId, postProductViewModel.StorageId);
            Assert.Equal(productViewModel.Name, postProductViewModel.Name);
            Assert.Equal(productViewModel.Quantity, postProductViewModel.Quantity);
            Assert.Equal(productViewModel.Size, postProductViewModel.Size);
            Assert.Equal(productViewModel.Width, postProductViewModel.Width);
        }
    }
}
