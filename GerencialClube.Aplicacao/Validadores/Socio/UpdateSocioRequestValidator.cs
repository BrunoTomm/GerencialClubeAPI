using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.Validators;
using FluentValidation;
using GerencialClube.Aplicacao.Validadores.Endereco;
using GerencialClube.Aplicacao.Validadores.Contato;

namespace GerencialClube.Aplicacao.Validadores.Cliente
{
    public class UpdateSocioRequestValidator : AbstractValidator<UpdateSocioRequest>
    {
        public UpdateSocioRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O Id do sócio é obrigatório.");

            RuleFor(c => c.Endereco)
                .SetValidator(new UpdateEnderecoRequestValidator())
                .When(c => c.Endereco != null);

            RuleForEach(c => c.Contatos)
                .SetValidator(new UpdateContatoRequestValidator());
        }
    }
}
