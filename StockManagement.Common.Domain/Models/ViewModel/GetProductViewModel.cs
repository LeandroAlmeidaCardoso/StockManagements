using System.ComponentModel.DataAnnotations;

namespace StockManagement.Common.Domain.Models.ViewModel;

public class GetProductViewModel
{
    [Required(ErrorMessage = "o campo deve ser diferente de nulo")]
    public string? Id { get; set; }

    public string? StorageId { get; set; }
}