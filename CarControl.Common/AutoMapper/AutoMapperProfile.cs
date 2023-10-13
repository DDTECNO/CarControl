using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Common.DTO.Autenticacao;
using CarControl.Common.ViewModel;
using CarControl.Domain;
using Microsoft.AspNetCore.Identity;

namespace CarControl.Common.AutoMapper
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

            base.CreateMap<LoginDTO, LoginViewModel>()
              .ReverseMap();

            base.CreateMap<RegistroDeUsuarioDTO, RegistroDeUsuarioViewModel>()
              .ReverseMap();

            base.CreateMap<IdentityUser, RegistroDeUsuarioDTO>()
                .ForMember(dest => dest.NmUsuario, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.NrTelefone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ReverseMap();



        }
    }
}
