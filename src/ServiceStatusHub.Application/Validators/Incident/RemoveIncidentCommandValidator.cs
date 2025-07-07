
using FluentValidation;
using ServiceStatusHub.Application.Commands.Incident;

namespace ServiceStatusHub.Application.Validators.Incident;

public class RemoveIncidentCommandValidator : AbstractValidator<RemoveIncidentCommand>
{
    public RemoveIncidentCommandValidator()
    {
        RuleFor(x => x.IncidentId)
            .NotEmpty()
            .WithMessage("Incident ID é obrigatório");

    }
}

