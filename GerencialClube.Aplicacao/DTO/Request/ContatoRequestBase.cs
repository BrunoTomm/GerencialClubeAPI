using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Aplicacao.DTO.Request
{
    public class ContatoRequestBase
    {
        public TipoContato Tipo { get; set; }
        public string Texto { get; set; }
    }
}
