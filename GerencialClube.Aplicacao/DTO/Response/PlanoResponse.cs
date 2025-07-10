using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Aplicacao.DTO.Response
{
    public class PlanoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<AreaClube> AreasPermitidas { get; set; }
    }
}
