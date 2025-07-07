using FluentValidation;
using ServiceStatusHub.Application.Commands.IncidentHistory;

namespace ServiceStatusHub.Application.Validators.IncidentHistory;

public class AddIncidentHistoryCommandValidator : AbstractValidator<AddIncidentHistoryCommand>
{
    public AddIncidentHistoryCommandValidator()
    {
        RuleFor(x => x.IncidentId)
            .NotEmpty().WithMessage("Id do incidente deve ser obrigatório");
        
        RuleFor(x => x.Action)
            .Must(action => Enum.IsDefined(typeof(Domain.Enums.IncidentAction), action))
            .WithMessage("Ação do histórico deve ser um valor válido.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição do histórico deve ser obrigatória");

        RuleFor(x => x.PerformedBy)
            .NotEmpty().WithMessage("Usuário que realizou a ação deve ser obrigatório")
            .MaximumLength(100).WithMessage("Usuário que realizou a ação deve possuir tamanho máximo de 100 caracteres");
    }
}
