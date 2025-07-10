using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Enumeradores;

public class RegistroAcesso : EntidadeBase
{
    public Guid SocioId { get; private set; }
    public AreaClube Area { get; private set; }
    public DateTime DataHora { get; private set; }
    public bool Autorizado { get; private set; }

    public Socio Socio { get; private set; }

    protected RegistroAcesso() { }

    public RegistroAcesso(Guid socioId, AreaClube area, bool autorizado)
    {
        SocioId = socioId;
        Area = area;
        DataHora = DateTime.UtcNow;
        Autorizado = autorizado;
    }
}
