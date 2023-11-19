using AutoMapper;
using GestaoCompras.Domain.Dtos;
using GestaoCompras.Domain.Entities;

namespace GestaoCompras.Domain.Mapper
{
    public class MapperConfig
    {
        public static IMapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Produto, ProdutoDto>();
                cfg.CreateMap<Compra, CompraDto>();
                cfg.CreateMap<ItemCompra, ItemCompraDto>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}