using FluentValidation;
using ServiceStatusHub.Application.Commands.IncidentHistory;

namespace ServiceStatusHub.Application.Validators.IncidentHistory
{
    public class RemoveIncidentHistoryCommandValidator : AbstractValidator<RemoveIncidentHistoryCommand>
    {
        public RemoveIncidentHistoryCommandValidator()
        {
            RuleFor(x => x.IncidentHistoryId)
                .NotEmpty().WithMessage("Id do histórico de incidente deve ser obrigatório")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Id do histórico de incidente deve ser um GUID válido.");
        }
    }
}
