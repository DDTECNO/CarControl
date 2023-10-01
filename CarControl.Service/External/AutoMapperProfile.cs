using AutoMapper;
using CarControl.Domain;
using CarControl.Domain.ViewModel;
using CarControl.Service.DTO;

namespace CarControl.Service.External
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            base.CreateMap<Veiculo, VeiculoDTO>()
                .ReverseMap();

            base.CreateMap<Vaga, VagaDTO>()
               .ReverseMap();

            base.CreateMap<Operacao, OperacaoDTO>()
               .ReverseMap();

            base.CreateMap<Movimento, MovimentoDTO>()
               .ReverseMap();

            base.CreateMap<MovimentoDTO, MovimentoViewModel>()
               .ReverseMap();

            base.CreateMap<VagaDTO, VagaViewModel>()
               .ReverseMap();

            base.CreateMap<VeiculoDTO, VeiculoViewModel>()
               .ReverseMap();
        }
    }
}
