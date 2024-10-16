using StockManagement.Common.Domain.Models.ViewModel;

namespace StockManagement.Common.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductViewModel> GetProductViewModel(GetProductViewModel model);
        Task<ProductViewModel> PostProductViewModel(PostProductViewModel model);
    }
}
