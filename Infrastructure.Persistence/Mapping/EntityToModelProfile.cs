using AutoMapper;
using CORE.Account.Domain.Model;
using Infrastructure.Persistence.Entity.Accounts;

namespace Infrastructure.Persistence.Mapping
{
    /// <summary>
    /// AutoMapper profile for entity-to-model mappings.
    /// Configured once and reused throughout the application for better performance.
    /// </summary>
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<MCliente, Cliente>()
                .ForMember(dest => dest.ClienteId, opt => opt.Ignore())
                .ForMember(dest => dest.Cuenta, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<MCuenta, Cuenta>()
                .ForMember(dest => dest.NumeroCuenta, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Movimientos, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<MMovimiento, Movimiento>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Cuenta, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
