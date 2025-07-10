using GerencialClube.Aplicacao.Validadores.Cliente;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using GerencialClube.Aplicacao.Validadores.Plano;

namespace GerencialClube.Aplicacao.Validators;

public static class ValidatorDependencyInjection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateSocioRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreatePlanoRequestValidator>();

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        return services;
    }
}
