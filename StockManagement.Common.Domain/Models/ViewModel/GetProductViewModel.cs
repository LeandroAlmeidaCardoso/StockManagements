using System.ComponentModel.DataAnnotations;

namespace StockManagement.Common.Domain.Models.ViewModel;

public class GetProductViewModel
{
    [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
    [Range(1, double.MaxValue, ErrorMessageResourceName = "InvalidProp",
        ErrorMessage = "o campo deve ser maior que 0")]
    public long? Id { get; set; }

    [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
    [Range(1, double.MaxValue, ErrorMessageResourceName = "InvalidProp",
         ErrorMessage = "o campo deve ser maior que 0")]
    public int? StorageId { get; set; }
}