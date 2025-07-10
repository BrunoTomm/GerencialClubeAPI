using GerencialClube.Dominio.Entidades;

namespace GerencialClube.Dominio.Extensoes
{
    public static class ContatoExtensions
    {
        public static void SincronizarContatos(this ICollection<Contato> contatosAtuais, List<Contato> novosContatos, Guid clienteId)
        {
            var contatosParaRemover = contatosAtuais
                .Where(c => !novosContatos.Any(n => n.Id == c.Id))
                .ToList();

            foreach (var contato in contatosParaRemover)
                contatosAtuais.Remove(contato);

            foreach (var novo in novosContatos)
            {
                var existente = contatosAtuais.FirstOrDefault(c => c.Id == novo.Id);

                if (existente != null)
                {
                    existente.Atualizar(novo.Tipo, novo.Texto);
                }
                else
                {
                    novo.DefinirSocio(clienteId);
                    contatosAtuais.Add(novo);
                }
            }
        }
    }
}
