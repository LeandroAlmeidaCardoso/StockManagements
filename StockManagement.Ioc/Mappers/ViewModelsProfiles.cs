#region

using AutoMapper;
using StockManagement.Common.Domain.Models.ViewModel;

#endregion

namespace StockManagement.Ioc.Mappers;

public class ViewModelsProfiles : Profile
{
    public ViewModelsProfiles()
    {
        CreateMap<PostProductViewModel, ProductViewModel>()
            .ReverseMap();
    }
}