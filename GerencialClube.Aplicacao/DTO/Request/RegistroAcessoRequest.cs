using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Aplicacao.DTO.Request
{
    public class RegistroAcessoRequest
    {
        public Guid SocioId { get; set; }
        public AreaClube Area { get; set; }
    }
}
