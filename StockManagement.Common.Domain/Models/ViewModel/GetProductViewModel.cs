using System.ComponentModel.DataAnnotations;

namespace StockManagement.Common.Domain.Models.ViewModel;

public class GetProductViewModel
{
    [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
    public string? StorageId { get; set; }
}