using GerencialClube.Aplicacao.DTO.Request;
using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Commands.RegistroAcesso
{
    public class RegistrarAcessoCommand : IRequest<RegistroAcessoResponse>
    {
        public RegistroAcessoRequest Request { get; }

        public RegistrarAcessoCommand(RegistroAcessoRequest request)
        {
            Request = request;
        }
    }
}
