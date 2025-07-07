using MediatR;
using ServiceStatusHub.Application.Commands.IncidentHistory;
using ServiceStatusHub.Domain.Entities;

public class AddIncidentHistoryCommandHandler : IRequestHandler<AddIncidentHistoryCommand, Guid>
{
    private readonly IIncidentHistoryRepository _historyRepository;

    public AddIncidentHistoryCommandHandler(IIncidentHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task<Guid> Handle(AddIncidentHistoryCommand request, CancellationToken cancellationToken)
    {
        var entry = new IncidentHistory(
            request.IncidentId,
            request.Action,
            request.Description,
            request.PerformedBy
        );

        await _historyRepository.AddAsync(entry);

        return entry.Id;
    }
}
