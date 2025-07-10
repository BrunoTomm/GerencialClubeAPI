using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Infra.Configuracoes;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace GerencialClube.Infra.Interfaces
{
    public class ViaCepApiService : IViaCepApiService
    {
        private readonly HttpClient _httpClient;

        public ViaCepApiService(
            HttpClient httpClient,
            IOptions<ApiSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ViaCepBaseUrl);
        }

        public async Task<EnderecoResponseViaCep> ObterEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"{cep}/json/");

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(content) || content.Contains("\"erro\":"))
            {
                return new EnderecoResponseViaCep
                {
                    Erro = "CEP não encontrado ou erro ao consultar o ViaCEP."
                };
            }

            var endereco = JsonSerializer.Deserialize<EnderecoResponseViaCep>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return endereco ?? new EnderecoResponseViaCep
            {
                Erro = "Não foi possível ler os dados do ViaCEP."
            };
        }


    }
}
