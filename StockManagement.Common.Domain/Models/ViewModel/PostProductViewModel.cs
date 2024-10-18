using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockManagement.Common.Domain.Models.ViewModel
{
    public class PostProductViewModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
        public string? StorageId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
        public string? Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
        [Range(0, double.MaxValue, ErrorMessageResourceName = "InvalidProp",
        ErrorMessage = "o campo deve ser diferente de nulo")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
        public decimal? Size { get; set; }

        [Required(ErrorMessageResourceName = "RequiredProp", ErrorMessage = "o campo deve ser diferente de nulo")]
        public decimal? Width { get; set; }
       
        [JsonIgnore]
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; } = DateTime.Now;
    }
}
