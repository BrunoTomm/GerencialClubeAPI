using GerencialClube.Aplicacao.DTO.Response;

namespace GerencialClube.Aplicacao.Interfaces
{
    public interface IViaCepApiService
    {
        Task<EnderecoResponseViaCep> ObterEnderecoPorCepAsync(string cep);
    }
}
