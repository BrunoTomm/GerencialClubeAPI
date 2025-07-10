using GerencialClube.Aplicacao.DTO.Request;
using GerencialClube.Aplicacao.DTO.Request.Create;
using FluentValidation;

namespace GerencialClube.Aplicacao.Validadores.Endereco;

public class CreateEnderecoRequestValidator : AbstractValidator<CreateEnderecoRequest>
{
    public CreateEnderecoRequestValidator()
    {
        RuleFor(e => e)
            .Must(e => e == null ||
                       !string.IsNullOrWhiteSpace(e.Cep) &&
                        !string.IsNullOrWhiteSpace(e.Numero))
            .WithMessage("Ou informe todos os campos obrigatórios do endereço ou envie nulo.");

        When(e => e != null, () =>
        {
            RuleFor(e => e.Cep)
                .NotEmpty().WithMessage("O campo CEP é obrigatório.")
                .Length(8).WithMessage("O CEP deve conter 8 caracteres.");
        });
    }
}
