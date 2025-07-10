namespace GerencialClube.Aplicacao.DTO.Request.Create
{
    public class CreateSocioRequest
    {
        public string Nome { get; set; } = string.Empty;
        public Guid PlanoId { get; set; }
        public CreateEnderecoRequest Endereco { get; set; }
        public List<CreateContatoRequest> Contatos { get; set; }
    }
}
