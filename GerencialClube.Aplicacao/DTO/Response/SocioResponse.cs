namespace GerencialClube.Aplicacao.DTO.Response
{
    public class SocioResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public PlanoResponse Plano { get; set; }

        public EnderecoResponse Endereco { get; set; }
        public List<ContatoResponse> Contatos { get; set; }
    }
}
