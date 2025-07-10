using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Aplicacao.Servicos;
using GerencialClube.Dominio.Mediador.Handlers;
using GerencialClube.Dominio.Repositorios;
using GerencialClube.Infra.Interfaces;
using GerencialClube.Infra.Repositorios;
using GerencialClube.Infra.Repositorios.Base;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerencialClube.Infra.Configuracoes
{
    public static class InjecaoDependencias
    {
        public static IServiceCollection AddInjecaoDependencias(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            services.AddScoped<ISocioService, SocioService>();
            services.AddScoped<IPlanoService, PlanoService>();
            services.AddScoped<IAcessoService, AcessoService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ISocioRepository, SocioRepository>();
            services.AddScoped<IPlanoRepository, PlanoRepository>();
            services.AddScoped<IRegistroAcessoRepository, RegistroAcessoRepository>();
            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            services.AddHttpClient<IViaCepApiService, ViaCepApiService>();

            services.AddMediatR(typeof(SocioCommandHandler).Assembly);

            return services;
        }
    }
}
