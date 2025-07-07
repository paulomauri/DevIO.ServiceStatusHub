using FluentValidation;
using ServiceStatusHub.Application.Commands.Service;

namespace ServiceStatusHub.Application.Validators.Service
{
    public class RemoveServiceCommandValidator : AbstractValidator<RemoveServiceCommand>
    {
        public RemoveServiceCommandValidator()
        {
            RuleFor(x => x.serviceId)
                .NotEmpty().WithMessage("Id do serviço deve ser obrigatório")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Id do serviço deve ser um GUID válido.");
        }
    }
}
