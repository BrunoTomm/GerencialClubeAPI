using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Enumeradores;

public class Plano : EntidadeBase
{
    public string Nome { get; private set; }
    public ICollection<AreaClube> AreasPermitidas { get; private set; }

    protected Plano() { }

    public Plano(string nome, List<AreaClube> areasPermitidas)
    {
        Nome = nome;
        AreasPermitidas = areasPermitidas ?? new List<AreaClube>();
    }

    public void AtualizarAreasPermitidas(List<AreaClube> novasAreas)
    {
        AreasPermitidas = novasAreas;
    }

    public void AtualizarNome(string novoNome)
    {
        Nome = novoNome;
    }
}
