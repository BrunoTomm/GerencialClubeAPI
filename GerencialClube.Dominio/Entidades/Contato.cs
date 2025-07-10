using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Dominio.Entidades
{
    public class Contato
    {
        public Guid Id { get; private set; }
        public TipoContato Tipo { get; private set; }
        public string Texto { get; private set; }

        public Guid SocioId { get; private set; }
        public Socio Socio { get; private set; }

        protected Contato() { }

        public Contato(TipoContato tipo, string texto)
        {
            Id = Guid.NewGuid();
            Tipo = tipo;
            Texto = texto;
        }

        public Contato(Guid id, TipoContato tipo, string texto)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Tipo = tipo;
            Texto = texto;
        }

        public void Atualizar(TipoContato tipo, string texto)
        {
            Tipo = tipo;
            Texto = texto;
        }

        public void DefinirSocio(Guid socioId)
        {
            SocioId = socioId;
        }
    }
}
