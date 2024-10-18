using StockManagement.Common.Domain.Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Tests.Common.Domain.ViewModel
{
    public class PostProductViewModelTests
    {
        private readonly PostProductViewModel _postProductViewModel;

        public PostProductViewModelTests() 
        {
            _postProductViewModel = new PostProductViewModel() 
            { 
                Id = new Guid().ToString(),
                StorageId = new Guid().ToString(),
                Name = "Teste",
                Quantity = 1,
                Size = 100,
                Width = 100,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
        }

        [Fact]
        public void PostProductViewModel_StorageIdIsNull_ValidationFails()
        {
            // Arrange
            var viewModelClone = _postProductViewModel.Clone();

            viewModelClone.StorageId = null;

            // Act
            var validationResults = ValidateModel(viewModelClone);

            // Assert
            Assert.Single(validationResults);
            Assert.Equal("o campo deve ser diferente de nulo", validationResults[0].ErrorMessage);
        }

        [Fact]
        public void PostProductViewModel_NameIsNull_ValidationFails()
        {
            // Arrange
            var viewModelClone = _postProductViewModel.Clone();

            viewModelClone.Name = null;

            // Act
            var validationResults = ValidateModel(viewModelClone);

            // Assert
            Assert.Single(validationResults);
            Assert.Equal("o campo deve ser diferente de nulo", validationResults[0].ErrorMessage);
        }

        [Fact]
        public void PostProductViewModel_QuantityIsNegative_ValidationFails()
        {
            // Arrange
            var viewModelClone = _postProductViewModel.Clone();

            viewModelClone.Quantity = -1;

            // Act
            var validationResults = ValidateModel(viewModelClone);

            // Assert
            Assert.Single(validationResults);
            Assert.Equal("o campo nao pode ser negativo", validationResults[0].ErrorMessage);
        }

        [Fact]
        public void PostProductViewModel_SizeIsNull_ValidationFails()
        {
            // Arrange
            var viewModelClone = _postProductViewModel.Clone();

            viewModelClone.Size = null;

            // Act
            var validationResults = ValidateModel(viewModelClone);

            // Assert
            Assert.Single(validationResults);
            Assert.Equal("o campo deve ser diferente de nulo", validationResults[0].ErrorMessage);
        }

        [Fact]
        public void PostProductViewModel_WidthIsNull_ValidationFails()
        {
            // Arrange
            var viewModelClone = _postProductViewModel.Clone();

            viewModelClone.Width = null;

            // Act
            var validationResults = ValidateModel(viewModelClone);

            // Assert
            Assert.Single(validationResults);
            Assert.Equal("o campo deve ser diferente de nulo", validationResults[0].ErrorMessage);
        }

        [Fact]
        public void PostProductViewModel_ValidModel_ValidationSucceeds()
        {
            // Act
            var validationResults = ValidateModel(_postProductViewModel);

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void PostProductViewModel_IdIsInitialized()
        {
            // Arrange
            var model = new PostProductViewModel();

            // Act & Assert
            Assert.False(string.IsNullOrEmpty(model.Id)); // Id should be generated
        }

        private static IList<ValidationResult> ValidateModel(PostProductViewModel model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}
