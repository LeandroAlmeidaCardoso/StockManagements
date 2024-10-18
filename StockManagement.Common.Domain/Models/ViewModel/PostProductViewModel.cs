using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockManagement.Common.Domain.Models.ViewModel
{
    public class PostProductViewModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "o campo deve ser diferente de nulo")]
        public string? StorageId { get; set; }

        [Required(ErrorMessage = "o campo deve ser diferente de nulo")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "o campo deve ser diferente de nulo")]
        [Range(0, double.MaxValue, ErrorMessage = "o campo nao pode ser negativo")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "o campo deve ser diferente de nulo")]
        public decimal? Size { get; set; }

        [Required(ErrorMessage = "o campo deve ser diferente de nulo")]
        public decimal? Width { get; set; }
       
        [JsonIgnore]
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        public PostProductViewModel Clone()
        {
            return (PostProductViewModel)this.MemberwiseClone();
        }
    }
}
