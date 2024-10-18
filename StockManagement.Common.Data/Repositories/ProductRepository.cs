using AutoMapper;
using StockManagement.Common.Domain.Interfaces;
using StockManagement.Common.Domain.Models.ViewModel;
using StockManagement.Common.Redis.Interfaces;
using System.Text.Json;

namespace StockManagement.Common.Data.Repositories
{
    public class ProductRepository(IRedisRepository redisRepository, IMapper mapper) : IProductRepository
    {
        private readonly IRedisRepository _redisRepository = redisRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductViewModel> GetProductViewModel(GetProductViewModel model)
        {
            var produtoSerializado = await _redisRepository.Buscar(model.Id!);

            if (produtoSerializado is null)
                throw new Exception("Produto Não Encontrado");

            return JsonSerializer.Deserialize<ProductViewModel>(produtoSerializado)!;
        }

        public async Task<ProductViewModel> PostProductViewModel(PostProductViewModel model)
        {
            var produtoSerializado = await _redisRepository.Buscar(model.Id!);

            if (produtoSerializado is not null)
                throw new Exception("Produto já existe");

            var pedidoViewModel = _mapper.Map<PostProductViewModel, ProductViewModel>(model);

            await _redisRepository.Adicionar(model.Id!, pedidoViewModel);

            return pedidoViewModel;
        }
    }
}
