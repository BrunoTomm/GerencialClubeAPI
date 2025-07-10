using AutoMapper;
using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Dominio.Entidades;

namespace GerencialClube.Aplicacao.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Socio
            CreateMap<CreateSocioRequest, Socio>();
            CreateMap<UpdateSocioRequest, Socio>();
            CreateMap<Socio, SocioResponse>()
                .ForMember(dest => dest.Plano, opt => opt.MapFrom(src => src.Plano));

            // Endereco
            CreateMap<CreateEnderecoRequest, Endereco>()
                .ForMember(dest => dest.Socio, opt => opt.Ignore())
                .ForMember(dest => dest.SocioId, opt => opt.Ignore());

            CreateMap<UpdateEnderecoRequest, Endereco>()
                .ForMember(dest => dest.Socio, opt => opt.Ignore())
                .ForMember(dest => dest.SocioId, opt => opt.Ignore());

            CreateMap<Endereco, EnderecoResponse>();

            // Contato
            CreateMap<CreateContatoRequest, Contato>()
                .ForMember(dest => dest.Socio, opt => opt.Ignore())
                .ForMember(dest => dest.SocioId, opt => opt.Ignore());

            CreateMap<UpdateContatoRequest, Contato>()
                .ForMember(dest => dest.Socio, opt => opt.Ignore())
                .ForMember(dest => dest.SocioId, opt => opt.Ignore());

            CreateMap<Contato, ContatoResponse>();

            // Plano
            CreateMap<CreatePlanoRequest, Plano>();
            CreateMap<UpdatePlanoRequest, Plano>();
            CreateMap<Plano, PlanoResponse>();
            CreateMap<PlanoResponse, Plano>();

            // Registro de Acesso
            CreateMap<RegistroAcesso, RegistroAcessoResponse>();
        }
    }
}
