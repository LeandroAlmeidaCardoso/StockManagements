using StockManagement.Common.Domain.Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace StockManagement.Tests.Common.Domain.ViewModel;
public class GetProductViewModelTests
{
    [Fact]
    public void GetProductViewModel_IdIsNull_ValidationFails()
    {
        // Arrange
        var model = new GetProductViewModel { Id = null };

        // Act
        var validationResults = ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal("o campo deve ser diferente de nulo", validationResults[0].ErrorMessage);
    }

    [Fact]
    public void GetProductViewModel_IdIsEmpty_ValidationFails()
    {
        // Arrange
        var model = new GetProductViewModel { Id = string.Empty };

        // Act
        var validationResults = ValidateModel(model);

        // Assert
        Assert.Single(validationResults);
        Assert.Equal("o campo deve ser diferente de nulo", validationResults[0].ErrorMessage);
    }

    [Fact]
    public void GetProductViewModel_IdIsValid_ValidationSucceeds()
    {
        // Arrange
        var model = new GetProductViewModel { Id = "valid-id" };

        // Act
        var validationResults = ValidateModel(model);

        // Assert
        Assert.Empty(validationResults);
    }
    private static IList<ValidationResult> ValidateModel(GetProductViewModel model)
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(model, context, results, true);

        // Retorna a lista de resultados, independentemente da validade
        return results;
    }

}