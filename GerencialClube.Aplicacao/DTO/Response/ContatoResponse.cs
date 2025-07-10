using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Aplicacao.DTO.Response
{
    public class ContatoResponse
    {
        public Guid Id { get; set; }
        public TipoContato Tipo { get; set; }
        public string Texto { get; set; }
    }
}
