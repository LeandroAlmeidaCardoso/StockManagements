using Microsoft.AspNetCore.Mvc;
using Moq;
using StockManagement.Common.Domain.Interfaces;
using StockManagement.Common.Domain.Models.ViewModel;
using StockManagement.Controllers;
using System.Dynamic;

namespace StockManagement.Tests.Microservices.Publishers.Api.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _controller = new ProductsController(_mockProductRepository.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithProductViewModel()
        {
            // Arrange
            var getProductViewModel = new GetProductViewModel { Id = "1" };
            var productViewModel = new ProductViewModel { Id = "1", Name = "Product1" };

            _mockProductRepository
                .Setup(repo => repo.GetProductViewModel(getProductViewModel))
                .ReturnsAsync(productViewModel);

            // Act
            var result = await _controller.Get(getProductViewModel);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<ProductViewModel>(okResult.Value);
            Assert.Equal(productViewModel.Id, returnValue.Id);
            Assert.Equal(productViewModel.Name, returnValue.Name);
        }

        [Fact]
        public async Task Post_ReturnsOkResult_WithProductViewModel()
        {
            // Arrange
            var postProductViewModel = new PostProductViewModel { Id = "1", Name = "Product1", Quantity = 10, StorageId = "Storage1" };
            var productViewModel = new ProductViewModel { Id = "1", Name = "Product1", Quantity = 10 };

            _mockProductRepository
                .Setup(repo => repo.PostProductViewModel(postProductViewModel))
                .ReturnsAsync(productViewModel);

            // Act
            var result = await _controller.Post(postProductViewModel);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductViewModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<ProductViewModel>(okResult.Value);
            Assert.Equal(productViewModel.Id, returnValue.Id);
            Assert.Equal(productViewModel.Name, returnValue.Name);
        }
    }
}
