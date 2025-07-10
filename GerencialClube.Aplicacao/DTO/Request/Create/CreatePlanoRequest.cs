using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Aplicacao.DTO.Request.Create
{
    public class CreatePlanoRequest
    {
        public string Nome { get; set; }
        public List<AreaClube> AreasPermitidas { get; set; }
    }
}
