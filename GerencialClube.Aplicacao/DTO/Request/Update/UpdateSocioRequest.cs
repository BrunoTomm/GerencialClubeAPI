namespace GerencialClube.Aplicacao.DTO.Request.Update
{
    public class UpdateSocioRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Guid PlanoId { get; set; }
        public UpdateEnderecoRequest Endereco { get; set; }
        public List<UpdateContatoRequest> Contatos { get; set; }
    }
}
