namespace StockManagement.Common.Domain.Models.ViewModel
{
    public class ProductViewModel
    {
        public string? Id { get; set; }
        public string? StorageId { get; set; }
        public string? Name { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Size { get; set; }
        public decimal? Width { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
