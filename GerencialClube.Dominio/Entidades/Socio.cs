using GerencialClube.Dominio.Entidades;
using System.Numerics;

public class Socio : EntidadeBase
{
    public string Nome { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public Endereco Endereco { get; private set; }
    public ICollection<Contato> Contatos { get; private set; }

    public Guid PlanoId { get; private set; }
    public Plano Plano { get; private set; }

    protected Socio() { }

    public Socio(string nome, Guid planoId)
    {
        Nome = nome;
        PlanoId = planoId;
        DataCadastro = DateTime.UtcNow;
        Contatos = new List<Contato>();
    }


    public void AtualizarNome(string nome) => Nome = nome;

    public void AtualizarContatos(List<Contato> novosContatos)
    {
        Contatos ??= new List<Contato>();

        var paraRemover = Contatos
            .Where(c => !novosContatos.Any(n => n.Id == c.Id))
            .ToList();

        foreach (var contato in paraRemover)
            Contatos.Remove(contato);

        foreach (var novo in novosContatos)
        {
            var existente = Contatos.FirstOrDefault(c => c.Id == novo.Id);
            if (existente != null)
                existente.Atualizar(novo.Tipo, novo.Texto);
            else
            {
                novo.DefinirSocio(Id); 
                Contatos.Add(novo);
            }
        }
    }

    public void AtualizarEndereco(Endereco endereco)
    {
        if (Endereco is null)
            Endereco = endereco;
        else
            Endereco.AtualizarEndereco(endereco);
    }

    public void AtualizarPlano(Plano novoPlano)
    {
        Plano = novoPlano ?? throw new ArgumentNullException(nameof(novoPlano));
        PlanoId = novoPlano.Id;
    }
}
