using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.Validators;
using FluentValidation;
using GerencialClube.Aplicacao.Validadores.Endereco;
using GerencialClube.Aplicacao.Validadores.Contato;

namespace GerencialClube.Aplicacao.Validadores.Cliente
{
    public class CreateSocioRequestValidator : AbstractValidator<CreateSocioRequest>
    {
        public CreateSocioRequestValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(c => c.PlanoId)
                .NotEmpty().WithMessage("O plano é obrigatório.");

            RuleFor(c => c.Endereco)
                .SetValidator(new CreateEnderecoRequestValidator())
                .When(c => c.Endereco != null);

            RuleForEach(c => c.Contatos)
                .SetValidator(new CreateContatoRequestValidator());
        }
    }
}
