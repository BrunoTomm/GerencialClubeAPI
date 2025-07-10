namespace GerencialClube.Aplicacao.DTO.Request.Update;

public class UpdateEnderecoRequest : EnderecoRequestBase
{
    public string Logradouro { get; set; }
    public string Cidade { get; set; }
}
