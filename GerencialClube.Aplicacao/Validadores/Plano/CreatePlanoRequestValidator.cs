using FluentValidation;
using GerencialClube.Aplicacao.DTO.Request.Create;

namespace GerencialClube.Aplicacao.Validadores.Plano
{
    public class CreatePlanoRequestValidator : AbstractValidator<CreatePlanoRequest>
    {
        public CreatePlanoRequestValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome do plano é obrigatório.");

            RuleFor(p => p.AreasPermitidas)
                .NotNull().WithMessage("As áreas permitidas são obrigatórias.")
                .Must(a => a.Any()).WithMessage("É necessário informar ao menos uma área permitida.");
        }
    }
}
