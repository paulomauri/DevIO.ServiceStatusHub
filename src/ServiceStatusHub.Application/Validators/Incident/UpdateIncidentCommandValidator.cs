using FluentValidation;
using ServiceStatusHub.Application.Commands.Incident;

namespace ServiceStatusHub.Application.Validators.Incident
{
    public class UpdateIncidentCommandValidator : AbstractValidator<UpdateIncidentCommand>
    {
        public UpdateIncidentCommandValidator()
        {
            RuleFor(x => x.incidentId)
                .NotEmpty()
                .WithMessage("Incident ID é obrigatório");

            RuleFor(x => x.serviceId)
                .NotEmpty()
                .WithMessage("Service ID é obrigatório");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status é obrigatório")
                .MaximumLength(50).WithMessage("Status não pode exceder 50 caracteres.");
        }
    }
}
