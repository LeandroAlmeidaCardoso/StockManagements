using AutoMapper;
using Moq;
using StockManagement.Common.Data.Repositories;
using StockManagement.Common.Domain.Models.ViewModel;
using StockManagement.Common.Redis.Interfaces;
using System.Text.Json;

namespace StockManagement.Tests.Common.Data.Repositories;
public class ProductRepositoryTests
{
    private readonly Mock<IRedisRepository> _mockRedisRepository;
    private readonly IMapper _mapper;
    private readonly ProductRepository _productRepository;

    public ProductRepositoryTests()
    {
        _mockRedisRepository = new Mock<IRedisRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PostProductViewModel, ProductViewModel>();
        });
        _mapper = config.CreateMapper();

        _productRepository = new ProductRepository(_mockRedisRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetProductViewModel_ProdutoEncontrado_RetornaViewModel()
    {
        // Arrange
        var model = new GetProductViewModel { Id = "1" };
        var productJson = JsonSerializer.Serialize(new ProductViewModel { Id = "1", Name = "Produto Teste" });
        _mockRedisRepository.Setup(repo => repo.Buscar(model.Id)).ReturnsAsync(productJson);

        // Act
        var result = await _productRepository.GetProductViewModel(model);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
        Assert.Equal("Produto Teste", result.Name);
    }

    [Fact]
    public async Task GetProductViewModel_ProdutoNaoEncontrado_LancaExcecao()
    {
        // Arrange
        var model = new GetProductViewModel { Id = "2" };
        _mockRedisRepository.Setup(repo => repo.Buscar(model.Id)).ReturnsAsync((string)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _productRepository.GetProductViewModel(model));
        Assert.Equal("Produto Não Encontrado", exception.Message);
    }

    [Fact]
    public async Task PostProductViewModel_ProdutoNaoExistente_AdicionaProduto()
    {
        // Arrange
        var model = new PostProductViewModel { Id = "1", Name = "Produto Teste" };
        _mockRedisRepository.Setup(repo => repo.Buscar(model.Id)).ReturnsAsync((string)null);

        // Act
        var result = await _productRepository.PostProductViewModel(model);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
        Assert.Equal("Produto Teste", result.Name);
        _mockRedisRepository.Verify(repo => repo.Adicionar(model.Id, It.IsAny<ProductViewModel>()), Times.Once);
    }

    [Fact]
    public async Task PostProductViewModel_ProdutoExistente_LancaExcecao()
    {
        // Arrange
        var model = new PostProductViewModel { Id = "1", Name = "Produto Teste" };
        var productJson = JsonSerializer.Serialize(new ProductViewModel { Id = "1", Name = "Produto Teste" });
        _mockRedisRepository.Setup(repo => repo.Buscar(model.Id)).ReturnsAsync(productJson);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _productRepository.PostProductViewModel(model));
        Assert.Equal("Produto já existe", exception.Message);
    }
}