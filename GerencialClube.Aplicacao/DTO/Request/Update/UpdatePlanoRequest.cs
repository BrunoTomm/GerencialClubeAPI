using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Aplicacao.DTO.Request.Update
{
    public class UpdatePlanoRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<AreaClube> AreasPermitidas { get; set; }
    }
}
