using FluentValidation;
using ServiceStatusHub.Application.Commands.Incident;

namespace ServiceStatusHub.Application.Validators.Incident;

public class CreateIncidentCommandValidator : AbstractValidator<CreateIncidentCommand>
{
    public CreateIncidentCommandValidator()
    {
        RuleFor(x => x.ServiceId)
            .NotEmpty()
            .WithMessage("Service ID é obrigatório");

        RuleFor(x => x.description)
            .NotEmpty()
            .WithMessage("Descrição é obrigatória")
            .MaximumLength(500)
            .WithMessage("Description não pode exceder 500 caracteres.");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status é obrigatório")
            .MaximumLength(50)
            .WithMessage("Status não pode exceder 50 caracteres.");
    }
}
