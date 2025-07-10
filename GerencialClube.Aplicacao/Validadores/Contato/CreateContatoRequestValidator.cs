using GerencialClube.Aplicacao.DTO.Request.Create;
using FluentValidation;

namespace GerencialClube.Aplicacao.Validadores.Contato
{
    public class CreateContatoRequestValidator : AbstractValidator<CreateContatoRequest>
    {
        public CreateContatoRequestValidator()
        {
        }
    }
}
