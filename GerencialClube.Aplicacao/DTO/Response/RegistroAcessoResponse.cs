using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Aplicacao.DTO.Response
{
    public class RegistroAcessoResponse
    {
        public Guid SocioId { get; set; }
        public AreaClube Area { get; set; }
        public DateTime DataHora { get; set; }
        public bool Autorizado { get; set; }
    }
}
