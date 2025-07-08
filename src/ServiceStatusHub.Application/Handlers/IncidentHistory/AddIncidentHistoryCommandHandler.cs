using MediatR;
using Microsoft.Extensions.Logging;
using ServiceStatusHub.Application.Commands.IncidentHistory;
using ServiceStatusHub.Domain.Entities;

public class AddIncidentHistoryCommandHandler : IRequestHandler<AddIncidentHistoryCommand, Guid>
{
    private readonly IIncidentHistoryRepository _historyRepository;
    private readonly ILogger<AddIncidentHistoryCommandHandler> _logger;
    public AddIncidentHistoryCommandHandler(IIncidentHistoryRepository historyRepository, ILogger<AddIncidentHistoryCommandHandler> logger)
    {
        _historyRepository = historyRepository;
        _logger = logger;
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

        _logger.LogInformation("Added incident history entry for IncidentId: {IncidentId}, Action: {Action}", 
            request.IncidentId, request.Action);

        return entry.Id;
    }
}
